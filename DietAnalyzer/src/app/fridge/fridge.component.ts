import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AuthService } from '../auth/services/auth.service';
import { BaseInfoService } from '../common/api/services/base-info.service';
import { CurrentIngredientsService } from '../common/api/services/current-ingredients.service';
import { CurrentProductsService } from '../common/api/services/current-products.service';
import { IngredientsBaseInfoService } from '../common/api/services/ingredients-base-info.service';
import { IngredientsExpensesService } from '../common/api/services/ingredients-expenses.service';
import { BaseInfo } from '../common/interfaces/base-info.interface';
import { CurrentIngredient } from '../common/interfaces/current-ingredient.interface';
import { CurrentProduct } from '../common/interfaces/current-product.interface';
import { IngredientBaseInfo } from '../common/interfaces/ingredient-base-info.interface';
import { IngredientsExpense } from '../common/interfaces/ingredients-expense.interface';
import { ProductBaseInfo } from '../common/interfaces/product-base-info.interface';
import { AddCurrentIngredientDialog } from '../shared/dialogs/add-current-ingr-dialog/add-current-ingr-dialog';
import { FillBaseInfoDialog } from '../shared/dialogs/fill-base-info-dialog/fill-base-info-dialog';

@Component({
  selector: 'app-fridge',
  templateUrl: './fridge.component.html',
  styleUrls: ['./fridge.component.css']
})
export class FridgeComponent implements OnInit {
  curI : CurrentIngredient[] = [];
  curP: CurrentProduct[] = [];

  baseInfo !: BaseInfo;
  ingrExpense !: IngredientsExpense;
  ingrIsExpired !: boolean;

  currentIngredients : IngredientBaseInfo[] = [];
  currentProducts : ProductBaseInfo[] = [];

  currentUserId !: string; 

  constructor(private currentIngredientsService: CurrentIngredientsService, private currentProductsService: CurrentProductsService,
    private authService: AuthService, public dialog: MatDialog, private ingredientsBaseInfoService: IngredientsBaseInfoService,
    private ingredientsExpensesService: IngredientsExpensesService, private baseInfoService: BaseInfoService) {
    this.loadIngredients();
    this.loadProducts();
  }

  ngOnInit(): void {
  }

  addCurIngredients() {
    const dialogRef = this.dialog.open(AddCurrentIngredientDialog);

    dialogRef.afterClosed().subscribe(result => {
      this.loadIngredients();
    });
  }

  loadIngredients() {
    this.authService.getCurrentUser().subscribe(res => {
      this.currentIngredients = [];
      this.currentUserId = res.id;

      if(this.currentUserId != null) {
        this.currentIngredientsService.getCurrentIngredientsByUserId(this.currentUserId).subscribe(ingr => {
          this.curI = ingr;

          this.curI.forEach(ingredient => {
            this.ingredientsBaseInfoService.getIngredientBaseInfoById(ingredient.ingredientId).subscribe(ingrBaseInfo => {
              this.currentIngredients.push(ingrBaseInfo);
            });
          })
        });
      }
    });
  }

  eatIngredient(ingr: IngredientBaseInfo) {
    if(ingr != null) {
      this.authService.getCurrentUser().subscribe(curUser => {
        this.currentIngredientsService.getCurrentIngredientByIngredientBaseInfoId(ingr.id, curUser.id).subscribe(curIngr => {
          this.currentIngredientsService.deleteCurrentIngredient(curIngr.id).subscribe(dCurIngr => {
            this.loadIngredients();
          });
        });
      });
    }
  }

  deleteIngredient(ingr: IngredientBaseInfo) {
    if(ingr != null) {
      this.authService.getCurrentUser().subscribe(curUser => {
        this.currentIngredientsService.getCurrentIngredientByIngredientBaseInfoId(ingr.id, curUser.id).subscribe(curIngr => {
          this.currentIngredientsService.deleteCurrentIngredient(curIngr.id).subscribe(delCurIngrRes => {
            
            this.ingredientsExpensesService.getIngredientsExpenseByIngredientBaseInfoId(ingr.id, curUser.id).subscribe(ingrExp => {
              this.ingredientsExpensesService.deleteIngredientsExpense(ingrExp.id).subscribe(delIngrExp => {

                this.ingredientsBaseInfoService.getIngredientBaseInfoById(curIngr.ingredientId).subscribe(ingrBInfo => {
                  this.ingredientsBaseInfoService.deleteIngredientBaseInfo(ingrBInfo.id).subscribe(delIngrBInfo => {

                    this.baseInfoService.getBaseInfoById(curIngr.baseInfoId).subscribe(bInfo => {
                      this.baseInfoService.deleteBaseInfo(bInfo.id).subscribe(dBInfo => {
                        this.loadIngredients();
                      });
                    })
                  });
                });
              });
            });
          });
        });
      });
    }
  }

  updateIngredient(ingr: IngredientBaseInfo) {
    if(ingr != null) {
      this.authService.getCurrentUser().subscribe(curUser => {
        this.currentIngredientsService.getCurrentIngredientByIngredientBaseInfoId(ingr.id, curUser.id).subscribe(curIngr => {
          this.baseInfoService.getBaseInfoById(curIngr.baseInfoId).subscribe(baseInfo => {
            this.ingredientsExpensesService.getIngredientsExpenseByIngredientBaseInfoId(ingr.id, curUser.id).subscribe(ingrExp => {
              const dialRef = this.dialog.open(FillBaseInfoDialog, {
                width: '350px',
                data: {amount: baseInfo.amount, unit: baseInfo.unit, expDate: baseInfo.expirationDate,
                  price: baseInfo.price, purDate: ingrExp.purchasingDate}
              });

              dialRef.afterClosed().subscribe(res => {
                this.baseInfo = {
                  id: curIngr.baseInfoId,
                  amount: res[0],
                  unit: res[1],
                  expirationDate: res[2],
                  price: res[3],
                  mealType: baseInfo.mealType
                };

                this.baseInfoService.updateBaseInfo(this.baseInfo).subscribe(updBI => {
                  if(Date.parse(res[2]) >= Date.now()) {
                    this.ingrIsExpired = false;
                  }
                  else {
                    this.ingrIsExpired = true;
                  }
  
                  this.ingrExpense = {
                    id: ingrExp.id,
                    purchasingDate: res[4],
                    ingredientId: ingr.id,
                    userId: curUser.id,
                    baseInfoId: baseInfo.id,
                    isExpired: this.ingrIsExpired
                  };

                  this.ingredientsExpensesService.updateIngredientsExpense(this.ingrExpense).subscribe();
                });
              });
            });
          });
        });
      });      
    }
  }

  addCurProducts() {
    
  }

  loadProducts() {
    this.authService.getCurrentUser().subscribe(res => {
      this.currentUserId = res.id;

      if(this.currentUserId != null) {
        this.currentProductsService.getCurrentProductsByUserId(this.currentUserId).subscribe(prods => {
          this.curP = prods;
        });
      }
    });
  }

  eatProduct(prod: ProductBaseInfo) {

  }

  deleteProduct(prod: ProductBaseInfo) {

  }

  updateProduct(prod: ProductBaseInfo) {

  }
}
