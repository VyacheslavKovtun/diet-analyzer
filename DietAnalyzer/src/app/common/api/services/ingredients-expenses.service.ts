import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { IngredientsExpense } from "../../interfaces/ingredients-expense.interface";

@Injectable()
export class IngredientsExpensesService
{
    url: string;

    constructor(private http: HttpClient){
        this.url = environment.API_URL + '/ingredients-expense';
    }

    getIngredientsExpenses() {
        return this.http.get<IngredientsExpense[]>(this.url, { withCredentials: true });
    }

    getIngredientsExpenseById(id: number) {
        return this.http.get<IngredientsExpense>(this.url + '/' + id, { withCredentials: true });
    }

    getIngredientsExpenseByIngredientBaseInfoId(infoId: number, curUserId: string) {
        return this.http.get<IngredientsExpense>(this.url + '/ingr-base-info/' + infoId + '/user/' + curUserId,
        { withCredentials: true });
    }

    getIngredientsExpensesByUserId(id: string) {
        return this.http.get<IngredientsExpense[]>(this.url + '/user/' + id, { withCredentials: true });
    }

    createIngredientsExpense(expense: IngredientsExpense) {
        var body = JSON.stringify(expense);
        var headerOptions = new HttpHeaders({'Content-Type':'application/json'});
        
        return this.http.post(this.url, body, {
            headers: headerOptions,
            withCredentials: true
        });
    }

    updateIngredientsExpense(expense: IngredientsExpense) {
        var body = JSON.stringify(expense);
        var headerOptions = new HttpHeaders({'Content-Type':'application/json'});

        return this.http.put(this.url, body, {
            headers: headerOptions,
            withCredentials: true
        });
    }

    deleteIngredientsExpense(id: number) {
        return this.http.delete(this.url + '/' + id, { withCredentials: true });
    }
}