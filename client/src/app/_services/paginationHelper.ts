import { HttpClient, HttpParams } from "@angular/common/http";
import { map } from "rxjs/operators";
import { PaginatedResult } from "../_models/pagination";

  // intern function
  export function getPaginatedResult<T>(url: string, params: HttpParams, http: HttpClient) {
    const paginatedResult: PaginatedResult<T> = new PaginatedResult<T>();
    return http.get<T>(url, { observe: 'response', params }).pipe(
      map((response) => {
        if (response.body)
          paginatedResult.result = response.body;
        else
          console.log('error pagination');

        if (response.headers.get('Pagination') !== null) {
          paginatedResult.pagination = JSON.parse(<string>response.headers.get('Pagination'));
        }
        return paginatedResult;
      })
    );
  }

  export function getPaginationHeaders(pageNumber: number, pageSize: number) {
    let params = new HttpParams();

    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    return params;
  }