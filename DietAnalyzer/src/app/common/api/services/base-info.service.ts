import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { BaseInfo } from "../../interfaces/base-info.interface";

@Injectable()
export class BaseInfoService
{
    url: string;

    constructor(private http: HttpClient){
        this.url = environment.API_URL + '/base-info';
    }

    getBaseInfo() {
        return this.http.get<BaseInfo[]>(this.url, { withCredentials: true });
    }

    getBaseInfoById(id: number) {
        return this.http.get<BaseInfo>(this.url + '/' + id, { withCredentials: true });
    }

    createBaseInfo(info: BaseInfo) {
        var body = JSON.stringify(info);
        var headerOptions = new HttpHeaders({'Content-Type':'application/json'});
        
        return this.http.post(this.url, body, {
            headers: headerOptions,
            withCredentials: true
        });
    }

    updateBaseInfo(info: BaseInfo) {
        var body = JSON.stringify(info);
        var headerOptions = new HttpHeaders({'Content-Type':'application/json'});

        return this.http.put(this.url, body, {
            headers: headerOptions,
            withCredentials: true
        });
    }

    deleteBaseInfo(id: number) {
        return this.http.delete(this.url + '/' + id, { withCredentials: true });
    }
}