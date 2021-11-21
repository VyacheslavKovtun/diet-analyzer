import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { CurrentProduct } from "../../interfaces/current-product.interface";

@Injectable()
export class CurrentProductsService
{
    url: string;

    constructor(private http: HttpClient){
        this.url = environment.API_URL + '/current-product';
    }

    getCurrentProducts() {
        return this.http.get<CurrentProduct[]>(this.url, { withCredentials: true });
    }

    getCurrentProductById(id: number) {
        return this.http.get<CurrentProduct>(this.url + '/' + id, { withCredentials: true });
    }

    getCurrentProductByUserId(id: string) {
        return this.http.get<CurrentProduct>(this.url + '/user/' + id, { withCredentials: true });
    }

    createCurrentProduct(product: CurrentProduct) {
        var body = JSON.stringify(product);
        var headerOptions = new HttpHeaders({'Content-Type':'application/json'});
        
        return this.http.post(this.url, body, {
            headers: headerOptions,
            withCredentials: true
        });
    }

    updateCurrentProduct(product: CurrentProduct) {
        var body = JSON.stringify(product);
        var headerOptions = new HttpHeaders({'Content-Type':'application/json'});

        return this.http.put(this.url, body, {
            headers: headerOptions,
            withCredentials: true
        });
    }

    deleteCurrentProduct(id: number) {
        return this.http.delete(this.url + '/' + id, { withCredentials: true });
    }
}