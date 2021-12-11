import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth/services/auth.service';
import { BaseInfoService } from '../common/api/services/base-info.service';
import { IngredientsBaseInfoService } from '../common/api/services/ingredients-base-info.service';
import { IngredientsExpensesService } from '../common/api/services/ingredients-expenses.service';
import { ProductsBaseInfoService } from '../common/api/services/products-base-info.service';
import { ProductsExpensesService } from '../common/api/services/products-expenses.service';
import { ExpenseInfo } from '../common/interfaces/expense-info.interface';

@Component({
  selector: 'app-expenses',
  templateUrl: './expenses.component.html',
  styleUrls: ['./expenses.component.css']
})
export class ExpensesComponent implements OnInit {
  usedIngredientsExpenses: ExpenseInfo[] = [];
  expiredIngredientsExpenses: ExpenseInfo[] = [];

  usedIngrSum !: number;
  expiredIngrSum !: number;

  usedProductsExpenses: ExpenseInfo[] = [];
  expiredProductsExpenses: ExpenseInfo[] = [];

  usedProdSum !: number;
  expiredProdSum !: number;

  constructor(private datePipe: DatePipe, private ingrExpensesService: IngredientsExpensesService, private ingredientsBaseInfoService: IngredientsBaseInfoService, private baseInfoService: BaseInfoService,
    private prodExpensesService: ProductsExpensesService, private productsBaseInfoService: ProductsBaseInfoService, private authService: AuthService) {
      this.loadIngrExpenses();
      this.loadProdExpenses();
  }

  ngOnInit(): void {
  }

  loadIngrExpenses() {
    this.usedIngredientsExpenses = [];
    this.usedIngrSum = 0;
    this.expiredIngredientsExpenses = [];
    this.expiredIngrSum = 0;

    this.authService.getCurrentUser().subscribe(curUser => {
      this.ingrExpensesService.getIngredientsExpensesByUserId(curUser.id).subscribe(ingrExpenses => {
        ingrExpenses.forEach(exp => {
          this.baseInfoService.getBaseInfoById(exp.baseInfoId).subscribe(bInfo => {
            this.ingredientsBaseInfoService.getIngredientBaseInfoById(exp.ingredientId).subscribe(ingrBInfo => {
              var isExpired = false;
              if(Date.parse(bInfo.expirationDate.toString()) >= Date.now()) {
                isExpired = false;
              }
              else {
                isExpired = true;
              }

              if(exp.isExpired !== isExpired) {
                exp.isExpired = isExpired;
                this.ingrExpensesService.updateIngredientsExpense(exp).subscribe();
              }

              var expInfo = {
                name: ingrBInfo.name,
                price: bInfo.price,
                purchasingDate: this.datePipe.transform(new Date(exp.purchasingDate), 'M.d.yy'),
                expirationDate: this.datePipe.transform(new Date(bInfo.expirationDate), 'M.d.yy'),
                isExpired: isExpired
              };

              if(!isExpired) {
                this.usedIngredientsExpenses.push(expInfo);
                this.usedIngrSum += expInfo.price;
              }
              else {
                this.expiredIngredientsExpenses.push(expInfo);
                this.expiredIngrSum += expInfo.price;
              }
            });
          });
        });
      })
    });
  }

  loadProdExpenses() {
    this.usedProductsExpenses = [];
    this.usedProdSum = 0;
    this.expiredProductsExpenses = [];
    this.expiredProdSum = 0;

    this.authService.getCurrentUser().subscribe(curUser => {
      this.prodExpensesService.getProductsExpensesByUserId(curUser.id).subscribe(prodExpenses => {
        prodExpenses.forEach(exp => {
          this.baseInfoService.getBaseInfoById(exp.baseInfoId).subscribe(bInfo => {
            this.productsBaseInfoService.getProductBaseInfoById(exp.productId).subscribe(prodBInfo => {
              var isExpired = false;
              if(Date.parse(bInfo.expirationDate.toString()) >= Date.now()) {
                isExpired = false;
              }
              else {
                isExpired = true;
              }

              if(exp.isExpired !== isExpired) {
                exp.isExpired = isExpired;
                this.prodExpensesService.updateProductsExpense(exp).subscribe();
              }

              var expInfo = {
                name: prodBInfo.title,
                price: bInfo.price,
                purchasingDate: this.datePipe.transform(new Date(exp.purchasingDate), 'M.d.yy'),
                expirationDate: this.datePipe.transform(new Date(bInfo.expirationDate), 'M.d.yy'),
                isExpired: isExpired
              };

              if(!isExpired) {
                this.usedProductsExpenses.push(expInfo);
                this.usedProdSum += expInfo.price;
              }
              else {
                this.expiredProductsExpenses.push(expInfo);
                this.expiredProdSum += expInfo.price;
              }
            });
          });
        });
      })
    });
  }
}
