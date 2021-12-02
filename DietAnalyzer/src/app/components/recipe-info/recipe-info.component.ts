import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/auth/services/auth.service';
import { FavouriteRecipesService } from 'src/app/common/api/services/favourite-recipes.service';
import { RecipesBaseInfoService } from 'src/app/common/api/services/recipes-base-info.service';
import { RecipesService } from 'src/app/common/api/services/recipes.service';
import { FavouriteRecipe } from 'src/app/common/interfaces/favourite-recipe.interface';
import { RecipeBaseInfo } from 'src/app/common/interfaces/recipe-base-info.interface';
import { RecipeInfo } from 'src/app/common/interfaces/recipe-info.interface';
import { AddedFavRecipeDialog } from 'src/app/shared/dialogs/added-fav-recipe-dialog/added-fav-recipe-dialog';
import { LikeExistsDialog } from 'src/app/shared/dialogs/like-exists-dialog/like-exists-dialog';
import { NecessaryAuthDialog } from 'src/app/shared/dialogs/necessary-auth-dialog/necessary-auth-dialog';

@Component({
  selector: 'app-recipe-info',
  templateUrl: './recipe-info.component.html',
  styleUrls: ['./recipe-info.component.css']
})
export class RecipeInfoComponent implements OnInit {
  recipeId !: number;
  recipe !: RecipeInfo;

  likedRecipe !: RecipeBaseInfo;
  currentUserId !: string;

  favedRecipe !: FavouriteRecipe;

  constructor(private activatedRoute: ActivatedRoute, private authService: AuthService, private favRecipesService: FavouriteRecipesService,
    private recipesService: RecipesService, private recipesBaseInfoService: RecipesBaseInfoService, public dialog: MatDialog) {
    this.activatedRoute.params.subscribe(p => {
      this.recipeId = p.recipeId;
      this.authService.getCurrentUser().subscribe(res => {
        this.currentUserId = res.id;
      });

      this.recipesService.getRecipeById(this.recipeId).subscribe(res => {
        this.recipe = res;
      });
    });
  }

  ngOnInit(): void {
  }

  like() {
    if(this.currentUserId != null) {
      if(this.recipe != null) {
        this.recipesBaseInfoService.getRecipeBaseInfoByApiId(this.recipeId).subscribe(r => {
          var dbRecipe = r;
          if(dbRecipe == null) {
            this.likedRecipe = {
              id: 0,
              apiId: this.recipe.id,
              title: this.recipe.title,
              calories: 0,
              imageUrl: this.recipe.image,
              imageType: this.recipe.imageType
            };
      
            this.recipesBaseInfoService.createRecipeBaseInfo(this.likedRecipe).subscribe(res => {
              this.recipesBaseInfoService.getRecipeBaseInfoByApiId(this.recipe.id).subscribe(rec => {
                if(rec.id != null) {
                  this.authService.getCurrentUser().subscribe(res => {
                    this.currentUserId = res.id;
      
                    this.favedRecipe = {
                      id: 0,
                      recipeId: rec.id,
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
}
