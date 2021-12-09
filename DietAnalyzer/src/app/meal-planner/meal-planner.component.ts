import { Component, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { AddPlannedRecipeDialog } from '../shared/dialogs/add-planned-recipe-dialog/add-planned-recipe-dialog';
import { MealPlannerService } from '../common/api/services/meal-planner.service';
import { Day, Item } from '../common/interfaces/week-plan.interface';

@Component({
  selector: 'app-meal-planner',
  templateUrl: './meal-planner.component.html',
  styleUrls: ['./meal-planner.component.css']
})
export class MealPlannerComponent implements OnInit {
  dates: string[] = [];
  days!: Day[];

  dayPlanRes: Item[] = [];

  constructor(private datePipe: DatePipe, public dialog: MatDialog, private mealPlannerService: MealPlannerService) {
    this.days = [];
    this.dayPlanRes = [];
    this.generateDates();
    this.loadWeekPlan();
  }

  ngOnInit(): void {

  }

  generateDates() {
    var today = new Date();
    for(let i = 1; i <= 7; i++) {
      var date = this.datePipe.transform(today, 'EEEE, M.d.yy');
      if(date != null) {
        this.dates.push(date);
      }
      today.setDate(today.getDate() + 1);
    }
  }

  loadWeekPlan() {
    var today = new Date();
    var startDate = this.datePipe.transform(today, 'yyyy-MM-dd');
    
    if(startDate != null) {
      this.mealPlannerService.getWeekPlan(startDate).subscribe(days => {
        this.days = [];
        this.days = days;
      });
    }
  }

  getDayPlan(day: string, slot: number) {
    this.dayPlanRes = [];
    if(this.days != null) {
      var convDate = new Date(new Date(day).toISOString());
      convDate.setUTCHours(0);
      var stamp = convDate.getTime() / 1000;
      
      var plan = this.days.find(d => d.date == stamp);

      var res = plan?.items.filter(i => i.slot == slot);
      if(res != null) {
        this.dayPlanRes = res;
      }

      return this.dayPlanRes?.sort((item1, item2) => {
        if(item1.position > item2.position) {
          return 1;
        }
        if(item1.position < item2.position) {
          return -1;
        }

        return 0;
      });
    }
    else return [];
  }

  deleteRecipe(item: Item) {
    console.log(item);
    this.mealPlannerService.deleteRecipeFromMealPlan(item.id).subscribe(delRes => {
      this.loadWeekPlan();
    });
  }

  addDayRecipe(day: string) {
    var conf = new MatDialogConfig();
    conf.id = day;
    const dialogRef = this.dialog.open(AddPlannedRecipeDialog, conf);

    dialogRef.afterClosed().subscribe(result => {
      this.loadWeekPlan();
    });
  }
}
