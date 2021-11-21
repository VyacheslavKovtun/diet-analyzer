import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { ProductsExpense } from "../../interfaces/products-expense.interface";

@Injectable()
export class ProductsExpensesService
{
    url: string;

    constructor(private http: HttpClient){
        this.url = environment.API_URL + '/products-expense';
    }

    getProductsExpenses() {
        return this.http.get<ProductsExpense[]>(this.url, { withCredentials: true });
    }

    getProductsExpenseById(id: number) {
        return this.http.get<ProductsExpense>(this.url + '/' + id, { withCredentials: true });
    }

    getProductsExpenseByUserId(id: string) {
        return this.http.get<ProductsExpense>(this.url + '/user/' + id, { withCredentials: true });
    }

    createProductsExpense(expense: ProductsExpense) {
        var body = JSON.stringify(expense);
        var headerOptions = new HttpHeaders({'Content-Type':'application/json'});
        
        return this.http.post(this.url, body, {
            headers: headerOptions,
            withCredentials: true
        });
    }

    updateProductsExpense(expense: ProductsExpense) {
        var body = JSON.stringify(expense);
        var headerOptions = new HttpHeaders({'Content-Type':'application/json'});

        return this.http.put(this.url, body, {
            headers: headerOptions,
            withCredentials: true
        });
    }

    deleteProductsExpense(id: number) {
        return this.http.delete(this.url + '/' + id, { withCredentials: true });
    }
}