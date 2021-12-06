import { Component, OnInit } from "@angular/core";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { MatDialogRef, MatDialog } from "@angular/material/dialog";
import { AuthService } from "src/app/auth/services/auth.service";
import { BaseInfoService } from "src/app/common/api/services/base-info.service";
import { CaloricInfoService } from "src/app/common/api/services/caloric-info.service";
import { CurrentProductsService } from "src/app/common/api/services/current-products.service";
import { ProductsBaseInfoService } from "src/app/common/api/services/products-base-info.service";
import { ProductsExpensesService } from "src/app/common/api/services/products-expenses.service";
import { ProductsStatisticService } from "src/app/common/api/services/products-statistic.service";
import { ProductsService } from "src/app/common/api/services/products.service";
import { BaseInfo } from "src/app/common/interfaces/base-info.interface";
import { CaloricInfo } from "src/app/common/interfaces/caloric-info.interface";
import { CurrentProduct } from "src/app/common/interfaces/current-product.interface";
import { ProductBaseInfo } from "src/app/common/interfaces/product-base-info.interface";
import { ProductStatistic } from "src/app/common/interfaces/product-statistic.interface";
import { ProductsExpense } from "src/app/common/interfaces/products-expense.interface";
import { ShortProduct } from "src/app/common/interfaces/short-product.interface";
import { EmptyFieldDialog } from "../empty-field-dialog/empty-field-dialog";
import { FillBaseInfoDialog } from "../fill-base-info-dialog/fill-base-info-dialog";

@Component({
    selector: 'add-current-product-dialog',
    templateUrl: 'add-current-product-dialog.html',
    styleUrls: ['add-current-product-dialog.css']
})
export class AddCurrentProductDialog implements OnInit{
    findProductForm !: FormGroup;
    products : ShortProduct[] = [];
    prodBaseInfo !: ProductBaseInfo;
    baseInfo !: BaseInfo;
    prodExpense !: ProductsExpense;
    currentProduct !: CurrentProduct;
    addingProd !: ShortProduct;
    caloricInfo !: CaloricInfo;
    productStatistic !: ProductStatistic;
  
    amount !: number;
    unit !: string;
    expDate !: Date;
    price !: number;
    purDate !: Date;
    isExpired !: boolean;
  
    fBIDialog !: MatDialogRef<FillBaseInfoDialog, any>;
  
    constructor(public dialogRef: MatDialogRef<AddCurrentProductDialog>, 
        private productsService: ProductsService, public dialog: MatDialog, private productsBaseInfoService: ProductsBaseInfoService,
        private baseInfoService: BaseInfoService, private authService: AuthService, private productsExpensesService: ProductsExpensesService,
        private currentProductsService: CurrentProductsService, private caloricInfoService: CaloricInfoService,
        private productsStatisticService: ProductsStatisticService) {}
  
    openDialog() {
        this.fBIDialog = this.dialog.open(FillBaseInfoDialog, {
          width: '350px',
          data: {amount: this.amount, unit: this.unit, expDate: this.expDate, price: this.price, purDate: this.purDate},
        });
    
        this.fBIDialog.afterClosed().subscribe(result => {
          this.amount = result[0];
          this.unit = result[1];
          this.expDate = result[2];
          this.price = result[3];
          this.purDate = result[4];
  
          this.save();
        });
    }
  
    addProduct(product: ShortProduct) {
      this.addingProd = product;
      this.openDialog();
    }
  
    save() {
      if(this.expDate != null) {
        if(Date.parse(this.expDate.toDateString()) >= Date.now()) {
          this.isExpired = false;
        }
        else {
          this.isExpired = true;
        }
      }
  
      if(this.addingProd != null) {
        this.prodBaseInfo = {
          id: 0,
          apiId: this.addingProd.id,
          title: this.addingProd.title,
          imageUrl: 'https://spoonacular.com/productImages/' + this.addingProd.id + '-90x90.' + this.addingProd.imageType,
          imageType: this.addingProd.imageType
        };
        this.openDialog();
  
        this.productsBaseInfoService.createProductBaseInfo(this.prodBaseInfo).subscribe(res => {
          this.productsService.getProductById(this.addingProd.id).subscribe(r => {
            this.baseInfo = {
              id: 0,
              price: this.price,
              amount: this.amount,
              expirationDate: this.expDate,
              unit: this.unit,
              mealType: r.aisle
            };
  
            this.baseInfoService.createBaseInfo(this.baseInfo).subscribe(crRes => {
              this.authService.getCurrentUser().subscribe(user => {
                this.productsBaseInfoService.getProductBaseInfoByApiId(this.addingProd.id).subscribe(prodInfo => {
                  this.baseInfoService.getBaseInfoByFields(this.baseInfo).subscribe(bInfo => {
                    this.prodExpense = {
                      id: 0,
                      productId: prodInfo.id,
                      userId: user.id,
                      baseInfoId: bInfo.id,
                      purchasingDate: this.purDate,
                      isExpired: this.isExpired
                    };
    
                    this.productsExpensesService.createProductsExpense(this.prodExpense).subscribe();

                    this.caloricInfo = {
                        id: 0,
                        calories: Number.parseInt(r.nutrition.caloricBreakdown.percentCarbs.toString()),
                        fat: Number.parseInt(r.nutrition.caloricBreakdown.percentFat.toString()),
                        protein: Number.parseInt(r.nutrition.caloricBreakdown.percentProtein.toString())
                    };
    
                    this.caloricInfoService.createCaloricInfo(this.caloricInfo).subscribe(cCIRes => {
                        this.caloricInfoService.getCaloricInfoByFields(this.caloricInfo).subscribe(calInfo => {
                            this.productStatistic = {
                                id: 0,
                                productId: prodInfo.id,
                                caloricInfoId: calInfo.id
                            };
                            
                            this.productsStatisticService.createProductStatistic(this.productStatistic).subscribe(cPSRes => {
                                this.currentProduct = {
                                    id: 0,
                                    productId: prodInfo.id,
                                    userId: user.id,
                                    baseInfoId: bInfo.id
                                };
                                this.currentProductsService.createCurrentProduct(this.currentProduct).subscribe();
                            });
                        });
                    });
                  });
                });
              });
            });
         });
        });
      }
  
      this.fBIDialog.close();
    }
  
    ngOnInit(): void {
      this.findProductForm = new FormGroup({
        "productTitle": new FormControl('', Validators.required)
      });
    }
  
    searchClick() {
      const field = this.findProductForm.value;
      var title = field.productTitle;
      
      if(title != '') {
        this.products = [];
  
        this.productsService.getProductsByTitle(title).subscribe(res => {
          this.products = res;
        });
      }
      else {
        this.dialog.open(EmptyFieldDialog);
      }
    }
  
    onCancelClick(): void {
      this.dialogRef.close();
    }
}