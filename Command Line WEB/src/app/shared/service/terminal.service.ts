import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TerminalService {

  private readonly _url = "localhost://terminal/";

  constructor(private _api: HttpClient) { }

  public async sendCommand(options: {command: string, args: string[]}): Promise<any>{
    return await this._api.post<any>(`${this._url}command`, options).toPromise();
  }
}
