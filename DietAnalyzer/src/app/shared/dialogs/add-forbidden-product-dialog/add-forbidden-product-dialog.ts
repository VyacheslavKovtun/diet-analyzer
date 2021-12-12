import { Component, OnInit} from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { AuthService } from 'src/app/auth/services/auth.service';
import { ForbiddenProductsService } from 'src/app/common/api/services/forbidden-products.service';
import { ProductsBaseInfoService } from 'src/app/common/api/services/products-base-info.service';
import { ProductsService } from 'src/app/common/api/services/products.service';
import { ProductBaseInfo } from 'src/app/common/interfaces/product-base-info.interface';
import { ShortProduct } from 'src/app/common/interfaces/short-product.interface';
import { EmptyFieldDialog } from '../empty-field-dialog/empty-field-dialog';

@Component({
  selector: 'add-forbidden-product-dialog',
  templateUrl: 'add-forbidden-product-dialog.html',
  styleUrls: ['add-forbidden-product-dialog.css']
})
export class AddForbiddenProductDialog implements OnInit{
  findProductForm !: FormGroup;
  products : ShortProduct[] = [];
  prodBaseInfo !: ProductBaseInfo;

  constructor(public dialogRef: MatDialogRef<AddForbiddenProductDialog>, 
    private productsService: ProductsService, public dialog: MatDialog, private productsBaseInfoService: ProductsBaseInfoService,
    private authService: AuthService, private forbiddenProductsService: ForbiddenProductsService) {}

  addProduct(prod: ShortProduct) {
    this.authService.getCurrentUser().subscribe(curUser => {
        if(curUser) {
            var prodBaseInfo = {
                id: 0,
                apiId: prod.id,
                title: prod.title,
                imageUrl: 'https://spoonacular.com/productImages/' + prod.id + '-90x90.' + prod.imageType,
                imageType: prod.imageType
            };
            
            this.productsBaseInfoService.createProductBaseInfo(prodBaseInfo).subscribe(cPBIRes => {
                this.productsBaseInfoService.getProductBaseInfoByApiId(prod.id).subscribe(prodBInfo => {
                    var forbiddenProduct = {
                        id: 0,
                        productId: prodBInfo.id,
                        userId: curUser.id
                    };
                    
                    this.forbiddenProductsService.createForbiddenProduct(forbiddenProduct).subscribe();
                });
            });
        }
    });
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