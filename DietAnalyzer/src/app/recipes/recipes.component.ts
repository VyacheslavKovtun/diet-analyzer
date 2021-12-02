import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../auth/services/auth.service';
import { FavouriteRecipesService } from '../common/api/services/favourite-recipes.service';
import { RecipesService } from '../common/api/services/recipes.service';
import { Recipe } from '../common/interfaces/recipe.interface';
import { RecipeBaseInfo } from '../common/interfaces/recipe-base-info.interface';
import { RecipesBaseInfoService } from '../common/api/services/recipes-base-info.service';
import { FavouriteRecipe } from '../common/interfaces/favourite-recipe.interface';
import { MatDialog } from '@angular/material/dialog';
import { EmptyFieldDialog } from '../shared/dialogs/empty-field-dialog/empty-field-dialog';
import { LikeExistsDialog } from '../shared/dialogs/like-exists-dialog/like-exists-dialog';
import { NoFavouriteRecipesDialog } from '../shared/dialogs/no-fav-recipes-dialog/no-fav-recipes-dialog';
import { AddedFavRecipeDialog } from '../shared/dialogs/added-fav-recipe-dialog/added-fav-recipe-dialog';
import { NecessaryAuthDialog } from '../shared/dialogs/necessary-auth-dialog/necessary-auth-dialog';

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
    private favRecipesService: FavouriteRecipesService, private recipesBaseInfoService: RecipesBaseInfoService, public dialog: MatDialog) {
    this.authService.isUserAuth$.subscribe(res => {
      this.authed = res;
    });
    this.authService.getCurrentUser().subscribe(res => {
      this.currentUserId = res.id;
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
      this.recipes = [];
      this.favRecipes = [];

      this.recipesService.getRecipesByTitle(title).subscribe(res => {
        this.recipes = res;
      });
    }
    else {
      this.dialog.open(EmptyFieldDialog);
    }
  }

  favouriteRecipesClick() {
    this.authService.getCurrentUser().subscribe(res => {
      this.favRecipes = [];
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
        else {
          this.dialog.open(NoFavouriteRecipesDialog);
        }
      });
    });
  }

  like(lRecipe: Recipe) {
    if(this.currentUserId != null) {
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
                    this.dialog.open(AddedFavRecipeDialog);
                  });
                }
              });
            });
          }
          else {
            this.dialog.open(LikeExistsDialog);
          }
        });
      }
    }
    else {
      this.dialog.open(NecessaryAuthDialog);
    }
  }

  deleteFromFavourite(favRecipe: RecipeBaseInfo) {
    if(favRecipe != null) {
      this.favRecipesService.getFavouriteRecipeByRecipeBaseInfoId(favRecipe.id).subscribe(res => {
        var dbFavRecipe = res;
        if(dbFavRecipe != null) {
          this.favRecipesService.deleteFavouriteRecipe(dbFavRecipe.id).subscribe(r => {
            this.recipesBaseInfoService.deleteRecipeBaseInfo(favRecipe.id).subscribe();
            this.favRecipes = [];
            this.favouriteRecipesClick();
          });
        }
      });
    }
  }
}
