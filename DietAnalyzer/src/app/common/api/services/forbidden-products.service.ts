import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { ForbiddenProduct } from "../../interfaces/forbidden-product.interface";

@Injectable()
export class ForbiddenProductsService
{
    url: string;

    constructor(private http: HttpClient){
        this.url = environment.API_URL + '/forbidden-product';
    }

    getForbiddenProducts() {
        return this.http.get<ForbiddenProduct[]>(this.url, { withCredentials: true });
    }

    getForbiddenProductById(id: number) {
        return this.http.get<ForbiddenProduct>(this.url + '/' + id, { withCredentials: true });
    }

    getForbiddenProductsByUserId(id: string) {
        return this.http.get<ForbiddenProduct[]>(this.url + '/user/' + id, { withCredentials: true });
    }

    createForbiddenProduct(product: ForbiddenProduct) {
        var body = JSON.stringify(product);
        var headerOptions = new HttpHeaders({'Content-Type':'application/json'});
        
        return this.http.post(this.url, body, {
            headers: headerOptions,
            withCredentials: true
        });
    }

    updateForbiddenProduct(product: ForbiddenProduct) {
        var body = JSON.stringify(product);
        var headerOptions = new HttpHeaders({'Content-Type':'application/json'});

        return this.http.put(this.url, body, {
            headers: headerOptions,
            withCredentials: true
        });
    }

    deleteForbiddenProduct(id: number) {
        return this.http.delete(this.url + '/' + id, { withCredentials: true });
    }
}