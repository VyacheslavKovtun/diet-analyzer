import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { CaloricInfo } from "../../interfaces/caloric-info.interface";

@Injectable()
export class CaloricInfoService
{
    url: string;

    constructor(private http: HttpClient){
        this.url = environment.API_URL + '/caloric-info';
    }

    getCaloricInfo() {
        return this.http.get<CaloricInfo[]>(this.url, { withCredentials: true });
    }

    getCaloricInfoById(id: number) {
        return this.http.get<CaloricInfo>(this.url + '/' + id, { withCredentials: true });
    }

    createCaloricInfo(info: CaloricInfo) {
        var body = JSON.stringify(info);
        var headerOptions = new HttpHeaders({'Content-Type':'application/json'});
        
        return this.http.post(this.url, body, {
            headers: headerOptions,
            withCredentials: true
        });
    }

    updateCaloricInfo(info: CaloricInfo) {
        var body = JSON.stringify(info);
        var headerOptions = new HttpHeaders({'Content-Type':'application/json'});

        return this.http.put(this.url, body, {
            headers: headerOptions,
            withCredentials: true
        });
    }

    deleteCaloricInfo(id: number) {
        return this.http.delete(this.url + '/' + id, { withCredentials: true });
    }
}