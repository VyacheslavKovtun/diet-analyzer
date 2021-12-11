import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable} from "@angular/core";
import { environment } from "src/environments/environment";
import { Aisle } from "../../interfaces/aisle.interface";
import { ShoppingItem } from "../../interfaces/shopping-item.interface";

@Injectable()
export class ShoppingListService
{
    url: string;

    constructor(private http: HttpClient){
        this.url = environment.API_URL + '/shopping-list';
    }

    getShoppingList() {
        return this.http.get<Aisle[]>(this.url, { withCredentials: true });
    }

    addToShoppingList(item: ShoppingItem) {
        var body = JSON.stringify(item);
        var headerOptions = new HttpHeaders({'Content-Type':'application/json'});
        
        return this.http.post(this.url, body, {
            headers: headerOptions,
            withCredentials: true
        });
    }

    deleteFromShoppingList(id: number) {
        return this.http.delete(this.url + '/delete/' + id, { withCredentials: true });
    }
}