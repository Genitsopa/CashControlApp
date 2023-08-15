import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';


@Injectable() // Remove providedIn: 'root'
export class CompanyApiRequest {
  private readonly baseUrl = 'http://localhost:5039/api/CompanyProduct'; // Update with your actual API base URL

  constructor(private http: HttpClient) { }

    getAllProducts() {
      return this.http.get(`${this.baseUrl}`);
    }
  
    getProductById(id: number) {
      return this.http.get(`${this.baseUrl}/Get/${id}`);
    }
  
    createProduct(product: any) {
      return this.http.post(`${this.baseUrl}`, product);
    }

    saveSalesRecords(salesRecords: any[]) {
      return this.http.post(`${this.baseUrl}/sales-records`, salesRecords);
    }

    updateProduct(id: number, product: any) {
      return this.http.put(`${this.baseUrl}/Update/${id}`, product);
    }
  
    deleteProduct(id: number) {
      return this.http.delete(`${this.baseUrl}/Delete/${id}`);
    }
    
    getSalesRecordsByDate(date: string) {
      return this.http.get(`${this.baseUrl}/sales-records?date=${date}`);
    }
    
  }
  
  