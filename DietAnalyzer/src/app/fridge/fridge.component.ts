import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AuthService } from '../auth/services/auth.service';
import { BaseInfoService } from '../common/api/services/base-info.service';
import { CaloricInfoService } from '../common/api/services/caloric-info.service';
import { CurrentIngredientsService } from '../common/api/services/current-ingredients.service';
import { CurrentProductsService } from '../common/api/services/current-products.service';
import { ForbiddenIngredientsService } from '../common/api/services/forbidden-ingredients.service';
import { ForbiddenProductsService } from '../common/api/services/forbidden-products.service';
import { IngredientsBaseInfoService } from '../common/api/services/ingredients-base-info.service';
import { IngredientsExpensesService } from '../common/api/services/ingredients-expenses.service';
import { ProductsBaseInfoService } from '../common/api/services/products-base-info.service';
import { ProductsExpensesService } from '../common/api/services/products-expenses.service';
import { ProductsStatisticService } from '../common/api/services/products-statistic.service';
import { BaseInfo } from '../common/interfaces/base-info.interface';
import { CurrentIngredient } from '../common/interfaces/current-ingredient.interface';
import { CurrentProduct } from '../common/interfaces/current-product.interface';
import { ForbiddenIngredient } from '../common/interfaces/forbidden-ingredient.interface';
import { ForbiddenProduct } from '../common/interfaces/forbidden-product.interface';
import { IngredientBaseInfo } from '../common/interfaces/ingredient-base-info.interface';
import { IngredientsExpense } from '../common/interfaces/ingredients-expense.interface';
import { ProductBaseInfo } from '../common/interfaces/product-base-info.interface';
import { ProductsExpense } from '../common/interfaces/products-expense.interface';
import { AddCurrentIngredientDialog } from '../shared/dialogs/add-current-ingr-dialog/add-current-ingr-dialog';
import { AddCurrentProductDialog } from '../shared/dialogs/add-current-product-dialog/add-current-product-dialog';
import { AddForbiddenIngredientDialog } from '../shared/dialogs/add-forbidden-ingredient-dialog/add-forbidden-ingredient-dialog';
import { AddForbiddenProductDialog } from '../shared/dialogs/add-forbidden-product-dialog/add-forbidden-product-dialog';
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
  isExpired !: boolean;
  prodExpense !: ProductsExpense;

  forbiddenIngredients : IngredientBaseInfo[] = [];
  forbiddenProducts : ProductBaseInfo[] = [];

  currentIngredients : IngredientBaseInfo[] = [];
  currentProducts : ProductBaseInfo[] = [];

  currentUserId !: string; 

  constructor(private currentIngredientsService: CurrentIngredientsService, private currentProductsService: CurrentProductsService,
    private authService: AuthService, public dialog: MatDialog, private ingredientsBaseInfoService: IngredientsBaseInfoService,
    private ingredientsExpensesService: IngredientsExpensesService, private baseInfoService: BaseInfoService,
    private productsBaseInfoService: ProductsBaseInfoService, private productsExpensesService: ProductsExpensesService,
    private productsStatisticService: ProductsStatisticService, private caloricInfoService: CaloricInfoService,
    private forbiddenIngredientsService: ForbiddenIngredientsService, private forbiddenProductsService: ForbiddenProductsService) {
    this.loadIngredients();
    this.loadForbiddenIngredients();
    this.loadProducts();
    this.loadForbiddenProducts();
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
                    this.isExpired = false;
                  }
                  else {
                    this.isExpired = true;
                  }
  
                  this.ingrExpense = {
                    id: ingrExp.id,
                    purchasingDate: res[4],
                    ingredientId: ingr.id,
                    userId: curUser.id,
                    baseInfoId: baseInfo.id,
                    isExpired: this.isExpired
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
    const dialogRef = this.dialog.open(AddCurrentProductDialog);

    dialogRef.afterClosed().subscribe(result => {
      this.loadProducts();
    });
  }

  loadProducts() {
    this.authService.getCurrentUser().subscribe(res => {
      this.currentProducts = [];
      this.currentUserId = res.id;

      if(this.currentUserId != null) {
        this.currentProductsService.getCurrentProductsByUserId(this.currentUserId).subscribe(prod => {
          this.curP = prod;

          this.curP.forEach(product => {
            this.productsBaseInfoService.getProductBaseInfoById(product.productId).subscribe(prodBaseInfo => {
              this.currentProducts.push(prodBaseInfo);
            });
          })
        });
      }
    });
  }

  eatProduct(prod: ProductBaseInfo) {
    if(prod != null) {
      this.authService.getCurrentUser().subscribe(curUser => {
        this.currentProductsService.getCurrentProductByProductBaseInfoId(prod.id, curUser.id).subscribe(curProd => {
          this.currentProductsService.deleteCurrentProduct(curProd.id).subscribe(dCurProd => {
            this.loadProducts();
          });
        });
      });
    }
  }

  deleteProduct(prod: ProductBaseInfo) {
    if(prod != null) {
      this.authService.getCurrentUser().subscribe(curUser => {
        this.currentProductsService.getCurrentProductByProductBaseInfoId(prod.id, curUser.id).subscribe(curProd => {
          this.currentProductsService.deleteCurrentProduct(curProd.id).subscribe(delCurProdRes => {
            
            this.productsExpensesService.getProductsExpenseByProductBaseInfoId(prod.id, curUser.id).subscribe(prodExp => {
              this.productsExpensesService.deleteProductsExpense(prodExp.id).subscribe(delProdExp => {

                this.productsStatisticService.getProductStatisticByProductBaseInfoId(prod.id).subscribe(prodStat => {
                  this.productsStatisticService.deleteProductStatistic(prodStat.id).subscribe(delProdStat => {

                    this.caloricInfoService.deleteCaloricInfo(prodStat.caloricInfoId).subscribe(delCalInfo => {
                      
                      this.productsBaseInfoService.getProductBaseInfoById(curProd.productId).subscribe(prodBInfo => {
                        this.productsBaseInfoService.deleteProductBaseInfo(prodBInfo.id).subscribe(delProdBInfo => {
      
                          this.baseInfoService.getBaseInfoById(curProd.baseInfoId).subscribe(bInfo => {
                            this.baseInfoService.deleteBaseInfo(bInfo.id).subscribe(dBInfo => {
                              this.loadProducts();
                            });
                          })
                        });
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
  }

  updateProduct(prod: ProductBaseInfo) {
    if(prod != null) {
      this.authService.getCurrentUser().subscribe(curUser => {
        this.currentProductsService.getCurrentProductByProductBaseInfoId(prod.id, curUser.id).subscribe(curProd => {
          this.baseInfoService.getBaseInfoById(curProd.baseInfoId).subscribe(baseInfo => {
            this.productsExpensesService.getProductsExpenseByProductBaseInfoId(prod.id, curUser.id).subscribe(prodExp => {
              const dialRef = this.dialog.open(FillBaseInfoDialog, {
                width: '350px',
                data: {amount: baseInfo.amount, unit: baseInfo.unit, expDate: baseInfo.expirationDate,
                  price: baseInfo.price, purDate: prodExp.purchasingDate}
              });

              dialRef.afterClosed().subscribe(res => {
                this.baseInfo = {
                  id: curProd.baseInfoId,
                  amount: res[0],
                  unit: res[1],
                  expirationDate: res[2],
                  price: res[3],
                  mealType: baseInfo.mealType
                };

                this.baseInfoService.updateBaseInfo(this.baseInfo).subscribe(updBI => {
                  if(Date.parse(res[2]) >= Date.now()) {
                    this.isExpired = false;
                  }
                  else {
                    this.isExpired = true;
                  }
  
                  this.prodExpense = {
                    id: prodExp.id,
                    purchasingDate: res[4],
                    productId: prod.id,
                    userId: curUser.id,
                    baseInfoId: baseInfo.id,
                    isExpired: this.isExpired
                  };

                  this.productsExpensesService.updateProductsExpense(this.prodExpense).subscribe();
                });
              });
            });
          });
        });
      });      
    }
  }

  loadForbiddenIngredients() {
    this.forbiddenIngredients = [];

    this.authService.getCurrentUser().subscribe(curUser => {
      if(curUser) {
        this.forbiddenIngredientsService.getForbiddenIngredientsByUserId(curUser.id).subscribe(forbIngrs => {
          forbIngrs.forEach(fI => {
            this.ingredientsBaseInfoService.getIngredientBaseInfoById(fI.ingredientId).subscribe(iBInfo => {
              this.forbiddenIngredients.push(iBInfo);
            });
          });
        });
      }
    });
  }

  addForbiddenIngredients() {
    const dialogRef = this.dialog.open(AddForbiddenIngredientDialog);

    dialogRef.afterClosed().subscribe(() => {
      this.loadForbiddenIngredients();
    });
  }

  deleteForbiddenIngredient(ingr: IngredientBaseInfo) {
    this.authService.getCurrentUser().subscribe(curUser => {
      if(curUser) {
        this.forbiddenIngredientsService.getForbiddenIngredientByIngredientBaseInfoId(ingr.id, curUser.id).subscribe(forbIngr => {
          this.forbiddenIngredientsService.deleteForbiddenIngredient(forbIngr.id).subscribe(() => {
            this.ingredientsBaseInfoService.deleteIngredientBaseInfo(ingr.id).subscribe(() => {
              this.loadForbiddenIngredients();
            });
          });
        })
      }
    });
  }

  loadForbiddenProducts() {
    this.forbiddenProducts = [];

    this.authService.getCurrentUser().subscribe(curUser => {
      if(curUser) {
        this.forbiddenProductsService.getForbiddenProductsByUserId(curUser.id).subscribe(forbProds => {
          forbProds.forEach(fP => {
            this.productsBaseInfoService.getProductBaseInfoById(fP.productId).subscribe(pBInfo => {
              this.forbiddenProducts.push(pBInfo);
            });
          });
        });
      }
    });
  }

  addForbiddenProducts() {
    const dialogRef = this.dialog.open(AddForbiddenProductDialog);

    dialogRef.afterClosed().subscribe(() => {
      this.loadForbiddenProducts();
    });
  }

  deleteForbiddenProduct(prod: ProductBaseInfo) {
    this.authService.getCurrentUser().subscribe(curUser => {
      if(curUser) {
        this.forbiddenProductsService.getForbiddenProductByProductBaseInfoId(prod.id, curUser.id).subscribe(forbProd => {
          this.forbiddenProductsService.deleteForbiddenProduct(forbProd.id).subscribe(() => {
            this.productsBaseInfoService.deleteProductBaseInfo(prod.id).subscribe(() => {
              this.loadForbiddenProducts();
            });
          });
        })
      }
    });
  }
}