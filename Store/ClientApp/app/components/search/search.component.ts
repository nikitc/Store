import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/toPromise';

class Product {
    
    constructor(public id?: number,
        public name?: string,) {
        
    }
}

@Component({
    selector: 'search-app',
    templateUrl: './search.component.html'
})
    
export class SearchComponent implements OnInit {
    items: Product[];

    constructor(private http: Http) { }
    ngOnInit() { }

    public entered = false;
    onSearchChange(newValue: string): void {
        this.entered = !(newValue === "");
        var self = this;
        this.http.get(`/api/products/SearchProductName=${newValue}`)
            .toPromise()
            .then(function mySuccess(response) {
                self.items = response.json().data as Product[];
            }, function myError(response) {
                console.log(response);
            });
    }
}
    