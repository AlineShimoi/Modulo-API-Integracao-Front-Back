import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Produto } from '../Models/Produto.Model';

@Injectable({ providedIn: 'root' })

export class ProdutoServices {
    private apiUrl = 'http://localhost:5156/api/produtos';
    
    constructor(private http: HttpClient) {}
    
    listarProdutos(): Observable<Produto[]> 
    {
        return this.http.get<Produto[]>(this.apiUrl);
    }
    
    buscarProdutoPorId(id: number): Observable<Produto> 
    {
        return this.http.get<Produto>(`${this.apiUrl}/${id}`);
    }
    
    criarProduto(produto: { nome: string; preco: number }):
    Observable<Produto> 
    {
        return this.http.post<Produto>(this.apiUrl, produto);
    }
    
    atualizarProduto(id: number, produto: { nome: string; preco: number }):
    Observable<Produto> 
    {
        return this.http.put<Produto>(`${this.apiUrl}/${id}`, produto);
    }

    removerProduto(id: number): Observable<void> 
    {
        return this.http.delete<void>(`${this.apiUrl}/${id}`);
    }
}
