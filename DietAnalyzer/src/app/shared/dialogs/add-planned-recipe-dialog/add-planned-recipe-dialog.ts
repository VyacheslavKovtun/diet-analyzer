import { DatePipe } from '@angular/common';
import {Component, OnInit} from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MealPlannerService } from 'src/app/common/api/services/meal-planner.service';
import { RecipesService } from 'src/app/common/api/services/recipes.service';
import { Recipe } from 'src/app/common/interfaces/recipe.interface';
import { SavingRecipe, Value } from 'src/app/common/interfaces/saving-recipe.interface';
import { EmptyFieldDialog } from '../empty-field-dialog/empty-field-dialog';
import { FillRecipeInfoDialog } from '../fill-recipe-info-dialog/fill-recipe-info-dialog';

@Component({
  selector: 'add-planned-recipe-dialog',
  templateUrl: 'add-planned-recipe-dialog.html',
  styleUrls: ['add-planned-recipe-dialog.css']
})
export class AddPlannedRecipeDialog implements OnInit{
  date !: string;

  findRecipeForm !: FormGroup;
  addingRecipe !: Recipe;

  savingRecipe !: SavingRecipe;
  value !: Value;

  recipes: Recipe[] = [];

  slot !: number;
  position !: number;

  fRIDialog !: MatDialogRef<FillRecipeInfoDialog, any>;

  constructor(private datePipe: DatePipe, public dialogRef: MatDialogRef<AddPlannedRecipeDialog>, public dialog: MatDialog,
    private mealPlannerService: MealPlannerService, private recipesService: RecipesService) {
        this.date = dialogRef.id;
    }

    openDialog() {
      this.fRIDialog = this.dialog.open(FillRecipeInfoDialog, {
        width: '350px',
        data: {slot: this.slot, position: this.position},
      });
  
      this.fRIDialog.afterClosed().subscribe(result => {
        this.slot = result[0];
        this.position = result[1];

        this.addToPlan();
      });
    }

    addRecipe(recipe: Recipe) {
        if(recipe != null) {
            this.addingRecipe = recipe;
            this.openDialog();
        }
    }

    addToPlan() {
        this.recipesService.getRecipeById(this.addingRecipe.id).subscribe(rec => {
            this.value = {
                id: this.addingRecipe.id,
                servings: rec.servings,
                title: this.addingRecipe.title,
                image: this.addingRecipe.image,
                imageType: rec.imageType
            };

            var convDate = new Date(this.date);
            convDate.setUTCHours(0);
            var stamp = convDate.getTime() / 1000;
    
            this.savingRecipe = {
                date: stamp,
                slot: this.slot,
                position: this.position,
                type: 'RECIPE',
                value: this.value
            };

            this.mealPlannerService.addRecipeToMealPlan(this.savingRecipe).subscribe(res => {
                this.fRIDialog.close();
            });
        });
    }

    ngOnInit(): void {
        this.findRecipeForm = new FormGroup({
            "recipeName": new FormControl('', Validators.required)
        });
    }

    searchClick() {
        const field = this.findRecipeForm.value;
        var name = field.recipeName;
        if(name != '') {
            this.recipes = [];

            this.recipesService.getRecipesByTitle(name).subscribe(res => {
                this.recipes = res;
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