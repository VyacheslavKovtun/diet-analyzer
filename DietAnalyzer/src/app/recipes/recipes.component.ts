import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../auth/services/auth.service';
import { FavouriteRecipesService } from '../common/api/services/favourite-recipes.service';
import { RecipesService } from '../common/api/services/recipes.service';
import { Recipe } from '../common/interfaces/recipe.interface';
import { RecipeBaseInfo } from '../common/interfaces/recipe-base-info.interface';
import { RecipesBaseInfoService } from '../common/api/services/recipes-base-info.service';
import { FavouriteRecipe } from '../common/interfaces/favourite-recipe.interface';

@Component({
  selector: 'app-recipes',
  templateUrl: './recipes.component.html',
  styleUrls: ['./recipes.component.css']
})
export class RecipesComponent implements OnInit {
  findRecipeForm !: FormGroup
  recipes !: Recipe[];
  favRecipes : RecipeBaseInfo[] = [];

  likedRecipe !: RecipeBaseInfo;
  favedRecipe !: FavouriteRecipe;

  authed = false;
  currentUserId !: string;

  constructor(private recipesService: RecipesService, private authService: AuthService,
    private favRecipesService: FavouriteRecipesService, private recipesBaseInfoService: RecipesBaseInfoService) {
    this.authService.isUserAuth$.subscribe(res => {
      this.authed = res;
    });
  }

  ngOnInit(): void {
    this.findRecipeForm = new FormGroup({
      "recipeName": new FormControl('', Validators.required)
    });
  }

  searchClick() {
    const field = this.findRecipeForm.value;
    var title = field.recipeName;
    if(title != '') {
      //TODO: modal asks to enter name
      this.recipesService.getRecipesByTitle(title).subscribe(res => {
        this.recipes = res;
      });
    }
  }

  favouriteRecipesClick() {
    this.authService.getCurrentUser().subscribe(res => {
      this.currentUserId = res.id;
      this.recipes = [];

      this.favRecipesService.getFavouriteRecipesByUserId(this.currentUserId).subscribe(rec => {
        if(rec.length != 0) {
          rec.forEach(r => {
            this.recipesBaseInfoService.getRecipeBaseInfoById(r.recipeId).subscribe(info => {
              this.favRecipes.push(info);
            });
          })
        }
      });
    });
  }

  like(lRecipe: Recipe) {
    if(lRecipe != null) {
      this.recipesBaseInfoService.getRecipeBaseInfoByApiId(lRecipe.id).subscribe(r => {
        var dbRecipe = r;

        if(dbRecipe == null) {
          this.likedRecipe = {
            id: 0,
            apiId: lRecipe.id,
            title: lRecipe.title,
            calories: 0,
            imageUrl: lRecipe.image,
            imageType: 'jpg'
          };
    
          this.recipesBaseInfoService.createRecipeBaseInfo(this.likedRecipe).subscribe(res => {
            this.recipesBaseInfoService.getRecipeBaseInfoByApiId(lRecipe.id).subscribe(recipe => {
              if(recipe.id != null) {
                this.authService.getCurrentUser().subscribe(res => {
                  this.currentUserId = res.id;
    
                  this.favedRecipe = {
                    id: 0,
                    recipeId: recipe.id,
                    userId: this.currentUserId
                  };
                  this.favRecipesService.createFavouriteRecipe(this.favedRecipe).subscribe();
                });
              }
            });
          });
        }
        //else modal message about already putting a like
      });
    }
  }

  deleteFromFavourite(favRecipe: RecipeBaseInfo) {
    console.log(favRecipe);
  }
}
