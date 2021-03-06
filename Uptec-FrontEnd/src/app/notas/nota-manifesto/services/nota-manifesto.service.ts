import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';
import { throwError, Observable } from 'rxjs';
import { BaseService } from './../../../shared/services/base.service';
import { UsuarioManifesto } from '../models/nota-manifesto';

@Injectable()
export class NotaManifestoService extends BaseService {

    constructor(private http: HttpClient) {
        super();
    }

    getPaged(pageNumber: number, pageSize: number): any {
        let url = `${this.API}/NotaEntrada/GetUnmanifested/${pageNumber}/${pageSize}`;
        return this.http.get<any>(url)
            .pipe(map((res: any) => this.extractData(res)),
                catchError(err => throwError(err)));
    }

    manifestar(manifestos: UsuarioManifesto[]) {
        return this.http.post(`${this.API}/NotaEntrada/ManifestarNfe`, manifestos)
            .pipe(map((res: any) => this.extractData(res)),
                catchError(err => throwError(err)));
    }

    GetUnmanifestedFromIntegration(): Observable<any[]> {
        return this.http.get<any[]>(`${this.API}/NotaEntrada/GetUnmanifestedFromIntegration`)
            .pipe(map((res : any) => this.extractData(res)), 
                  catchError(err => throwError(err))
                  );
    }

    /* upload(arquivo: FormData) {
        return this.http.post(`${this.API}/NotaEntrada`, arquivo);
    }

    getPaged(pageNumber: number, pageSize: number, tipoEmissor: number, status: number, 
        startDate: string, endDate: string, search: string): any {
        let url = search ? 
        `${this.API}/NotaEntrada/${pageNumber}/${pageSize}?tipoEmissor=${tipoEmissor}&status=${status}&startDate=${startDate}&endDate=${endDate}&search=${search}` :
        `${this.API}/NotaEntrada/${pageNumber}/${pageSize}?tipoEmissor=${tipoEmissor}&status=${status}&startDate=${startDate}&endDate=${endDate}`;
        return this.http.get<any>(url)
            .pipe(catchError(err => throwError(err)));
    }

    getByIdConsisted(id: string) : any {
        let url = `${this.API}/NotaEntrada/GetFullByIdConsisted/${id}`;
        return this.http.get<NotaEntrada>(url)
            .pipe(map((res : any) => this.extractData(res)), 
                catchError(err => throwError(err)));
    }

    getNotasClientesConciliar(): Observable<EnumType[]> {
        return this.http.get<EnumType[]>(`${this.API}/NotaEntrada/GetNotasClientesConciliar`)
            .pipe(map((res : any) => this.extractData(res)), 
                  catchError(err => throwError(err))
                  );
    }

    definirTipoEmissor(nota: NotaEntradaTipoEmissor) {
        return this.http.put(`${this.API}/NotaEntrada/DefinirTipoEmissor`, nota); 
    }

    conciliar(nota: NotaEntrada) {
        return this.http.put(`${this.API}/NotaEntrada/Conciliar`, nota);
    }

    cobrir(conciliacao: any) {
        return this.http.put(`${this.API}/NotaEntrada/Cobrir`, conciliacao);
    }

    delete(id : string) {
        return this.http.delete(`${this.API}/NotaEntrada/${id}`);
    } */
}