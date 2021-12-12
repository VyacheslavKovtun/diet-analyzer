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
import { RouterModule } from '@angular/router';

import {A11yModule} from '@angular/cdk/a11y';
import {CdkAccordionModule} from '@angular/cdk/accordion';
import {ClipboardModule} from '@angular/cdk/clipboard';
import {DragDropModule} from '@angular/cdk/drag-drop';
import {PortalModule} from '@angular/cdk/portal';
import {ScrollingModule} from '@angular/cdk/scrolling';
import {CdkStepperModule} from '@angular/cdk/stepper';
import {CdkTableModule} from '@angular/cdk/table';
import {CdkTreeModule} from '@angular/cdk/tree';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import {MatBadgeModule} from '@angular/material/badge';
import {MatBottomSheetModule} from '@angular/material/bottom-sheet';
import {MatButtonModule} from '@angular/material/button';
import {MatButtonToggleModule} from '@angular/material/button-toggle';
import {MatCardModule} from '@angular/material/card';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatChipsModule} from '@angular/material/chips';
import {MatStepperModule} from '@angular/material/stepper';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatDialog, MatDialogClose, MatDialogModule} from '@angular/material/dialog';
import {MatDividerModule} from '@angular/material/divider';
import {MatExpansionModule} from '@angular/material/expansion';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatIconModule} from '@angular/material/icon';
import {MatInputModule} from '@angular/material/input';
import {MatListModule} from '@angular/material/list';
import {MatMenuModule} from '@angular/material/menu';
import {MatNativeDateModule, MatRippleModule} from '@angular/material/core';
import {MatPaginatorModule} from '@angular/material/paginator';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {MatRadioModule} from '@angular/material/radio';
import {MatSelectModule} from '@angular/material/select';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatSliderModule} from '@angular/material/slider';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {MatSortModule} from '@angular/material/sort';
import {MatTableModule} from '@angular/material/table';
import {MatTabsModule} from '@angular/material/tabs';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatTooltipModule} from '@angular/material/tooltip';
import {MatTreeModule} from '@angular/material/tree';
import {OverlayModule} from '@angular/cdk/overlay';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from './auth/services/auth.service';
import { HttpClientModule } from '@angular/common/http';
import { ApiUsersService } from './common/api/services/api.users.service';
import { RecipesService } from './common/api/services/recipes.service';
import { FavouriteRecipesService } from './common/api/services/favourite-recipes.service';
import { RecipesBaseInfoService } from './common/api/services/recipes-base-info.service';
import { RecipeInfoComponent } from './components/recipe-info/recipe-info.component';
import { CurrentIngredientsService } from './common/api/services/current-ingredients.service';
import { CurrentProductsService } from './common/api/services/current-products.service';
import { IngredientsService } from './common/api/services/ingredients.service';
import { AddCurrentIngredientDialog } from './shared/dialogs/add-current-ingr-dialog/add-current-ingr-dialog';
import { IngredientsBaseInfoService } from './common/api/services/ingredients-base-info.service';
import { FillBaseInfoDialog } from './shared/dialogs/fill-base-info-dialog/fill-base-info-dialog';
import { BaseInfoService } from './common/api/services/base-info.service';
import { CaloricInfoService } from './common/api/services/caloric-info.service';
import { IngredientsStatisticService } from './common/api/services/ingredients-statistic.service';
import { IngredientsExpensesService } from './common/api/services/ingredients-expenses.service';
import { ProductsService } from './common/api/services/products.service';
import { ProductsExpensesService } from './common/api/services/products-expenses.service';
import { ProductsBaseInfoService } from './common/api/services/products-base-info.service';
import { ProductsStatisticService } from './common/api/services/products-statistic.service';
import { AddCurrentProductDialog } from './shared/dialogs/add-current-product-dialog/add-current-product-dialog';
import { DatePipe } from '@angular/common';
import { AddPlannedRecipeDialog } from './shared/dialogs/add-planned-recipe-dialog/add-planned-recipe-dialog';
import { MealPlannerService } from './common/api/services/meal-planner.service';
import { FillRecipeInfoDialog } from './shared/dialogs/fill-recipe-info-dialog/fill-recipe-info-dialog';
import { ShoppingListService } from './common/api/services/shopping-list.service';
import { AddShoppingListProductDialog } from './shared/dialogs/add-shopping-list-product-dialog/add-shopping-list-product-dialog';
import { AddShoppingListItemDialog } from './shared/dialogs/add-shopping-list-item-dialog/add-shopping-list-item-dialog';
import { AuthGuard } from './auth/guards/auth.guard';
import { ForbiddenIngredientsService } from './common/api/services/forbidden-ingredients.service';
import { ForbiddenProductsService } from './common/api/services/forbidden-products.service';
import { AddForbiddenIngredientDialog } from './shared/dialogs/add-forbidden-ingredient-dialog/add-forbidden-ingredient-dialog';
import { AddForbiddenProductDialog } from './shared/dialogs/add-forbidden-product-dialog/add-forbidden-product-dialog';

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
    ErrorComponent,
    RecipeInfoComponent,
    AddCurrentIngredientDialog,
    FillBaseInfoDialog,
    AddCurrentProductDialog,
    AddPlannedRecipeDialog,
    FillRecipeInfoDialog,
    AddShoppingListProductDialog,
    AddShoppingListItemDialog,
    AddForbiddenIngredientDialog,
    AddForbiddenProductDialog,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'fridge', component: FridgeComponent, canActivate: [AuthGuard] },
      { path: 'recipes', component: RecipesComponent },
      { path: 'info/:recipeId', component: RecipeInfoComponent },
      { path: 'meal-planner', component: MealPlannerComponent, canActivate: [AuthGuard] },
      { path: 'shopping-list', component: ShoppingListComponent, canActivate: [AuthGuard] },
      { path: 'expenses', component: ExpensesComponent, canActivate: [AuthGuard] },
      { path: 'login', component: LoginComponent},
      { path: '**', component: ErrorComponent },
    ]),
    A11yModule,
    CdkAccordionModule,
    ClipboardModule,
    CdkStepperModule,
    CdkTableModule,
    CdkTreeModule,
    DragDropModule,
    MatAutocompleteModule,
    MatBadgeModule,
    MatBottomSheetModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatCardModule,
    MatCheckboxModule,
    MatChipsModule,
    MatStepperModule,
    MatDatepickerModule,
    MatDialogModule,
    MatDividerModule,
    MatExpansionModule,
    MatGridListModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatMenuModule,
    MatNativeDateModule,
    MatPaginatorModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    MatRadioModule,
    MatRippleModule,
    MatSelectModule,
    MatSidenavModule,
    MatSliderModule,
    MatSlideToggleModule,
    MatSnackBarModule,
    MatSortModule,
    MatTableModule,
    MatTabsModule,
    MatToolbarModule,
    MatTooltipModule,
    MatTreeModule,
    OverlayModule,
    PortalModule,
    ScrollingModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
  ],
  providers: [ AuthService, ApiUsersService, RecipesService, FavouriteRecipesService,
    CurrentIngredientsService, RecipesBaseInfoService, CurrentProductsService, IngredientsService,
    IngredientsBaseInfoService, BaseInfoService, CaloricInfoService, IngredientsStatisticService, 
    IngredientsExpensesService, ProductsService, ProductsExpensesService, ProductsBaseInfoService,
    ProductsStatisticService, DatePipe, MealPlannerService, ShoppingListService, AuthGuard,
    ForbiddenIngredientsService, ForbiddenProductsService],
  bootstrap: [AppComponent]
})
export class AppModule { }
