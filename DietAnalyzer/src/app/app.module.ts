import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { RecipesComponent } from './recipes/recipes.component';
import { MealPlannerComponent } from './meal-planner/meal-planner.component';
import { FridgeComponent } from './fridge/fridge.component';
import { ExpensesComponent } from './expenses/expenses.component';
import { ShoppingListComponent } from './shopping-list/shopping-list.component';
import { ErrorComponent } from './components/error/error.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LoginComponent,
    HomeComponent,
    RecipesComponent,
    MealPlannerComponent,
    FridgeComponent,
    ExpensesComponent,
    ShoppingListComponent,
    ErrorComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
