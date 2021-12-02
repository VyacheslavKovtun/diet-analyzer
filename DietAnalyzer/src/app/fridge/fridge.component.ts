import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AuthService } from '../auth/services/auth.service';
import { CurrentIngredientsService } from '../common/api/services/current-ingredients.service';
import { CurrentProductsService } from '../common/api/services/current-products.service';
import { IngredientsBaseInfoService } from '../common/api/services/ingredients-base-info.service';
import { CurrentIngredient } from '../common/interfaces/current-ingredient.interface';
import { CurrentProduct } from '../common/interfaces/current-product.interface';
import { IngredientBaseInfo } from '../common/interfaces/ingredient-base-info.interface';
import { ShortIngredient } from '../common/interfaces/short-ingredient.interface';
import { AddCurrentIngredientDialog } from '../shared/dialogs/add-current-ingr-dialog/add-current-ingr-dialog';

@Component({
  selector: 'app-fridge',
  templateUrl: './fridge.component.html',
  styleUrls: ['./fridge.component.css']
})
export class FridgeComponent implements OnInit {
  curI : CurrentIngredient[] = [];
  curP: CurrentProduct[] = [];

  currentIngredients : IngredientBaseInfo[] = [];

  currentUserId !: string; 

  constructor(private currentIngredientsService: CurrentIngredientsService, private currentProductsService: CurrentProductsService,
    private authService: AuthService, public dialog: MatDialog, private ingredientsBaseInfoService: IngredientsBaseInfoService) {
    this.loadIngredients();
    this.loadProducts();
  }

  ngOnInit(): void {
  }

  addCurIngredients() {
    this.dialog.open(AddCurrentIngredientDialog);
    this.loadIngredients();
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

  deleteIngredient(ingr: IngredientBaseInfo) {

  }

  updateIngredient(ingr: IngredientBaseInfo) {

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
}
