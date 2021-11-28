import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { RecipesService } from 'src/app/common/api/services/recipes.service';
import { RecipeInfo } from 'src/app/common/interfaces/recipe-info.interface';

@Component({
  selector: 'app-recipe-info',
  templateUrl: './recipe-info.component.html',
  styleUrls: ['./recipe-info.component.css']
})
export class RecipeInfoComponent implements OnInit {
  recipeId !: number;
  recipe !: RecipeInfo;

  constructor(private activatedRoute: ActivatedRoute, private recipesService: RecipesService) {
    this.activatedRoute.params.subscribe(p => {
      this.recipeId = p.recipeId;
      this.recipesService.getRecipeById(this.recipeId).subscribe(res => {
        this.recipe = res;
      });
    });
  }

  ngOnInit(): void {
  }

  like() {
    
  }
}
