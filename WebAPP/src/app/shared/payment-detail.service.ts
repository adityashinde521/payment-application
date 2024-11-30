import { PaymentDetail } from './payment-detail.model';
import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class PaymentDetailService {
  formData: PaymentDetail= {
    CVV: null,
    CardNumber: null,
    CardOwnerName: null,
    ExpirationDate: null,
    PMId: null
  };
  
  //readonly rootURL = 'http://localhost:56287/api';
  readonly rootURL = 'http://localhost:5000/api';  //dockerapi
  
  list : PaymentDetail[];
  templist;
  constructor(private http: HttpClient) { }

  postPaymentDetail() {
    return this.http.post(this.rootURL + '/PaymentDetail', this.formData);
  }
  putPaymentDetail() {
    return this.http.put(this.rootURL + '/PaymentDetail/'+ this.formData.PMId, this.formData);
  }
  deletePaymentDetail(id) {
    return this.http.delete(this.rootURL + '/PaymentDetail/'+ id);
  }

  refreshList(){
    this.http.get(this.rootURL + '/PaymentDetail')
    .toPromise()
    .then(res => this.list = res as PaymentDetail[]);
  }
  refreshListt() {
    this.http.get(this.rootURL + '/Values')
      .toPromise()
      .then(res => {
        this.templist = res;  // Assign the response to templist
        console.log("Temp Values", this.templist);  // Log the response values
      })
      .catch(error => {
        console.error("Error fetching temp values from API:", error);  // Handle any errors
      });
  }
}
