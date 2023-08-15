import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-leagues',
  templateUrl: './leagues.component.html',
  styleUrls: ['./leagues.component.css']
})
export class LeaguesComponent implements OnInit {
  leagues: any[] | undefined;
  form: any = {};

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getLeagues();
  }

  getLeagues(): void {
    this.http.get<any[]>('/api/leagues').subscribe(leagues => {
      this.leagues = leagues;
    });
  }

  createLeague(league: any): void {
    this.http.post<any>('/api/leagues', league).subscribe(() => {
      this.getLeagues();
    });
  }

  updateLeague(league: any): void {
    this.http.put<any>(`/api/leagues/${league.id}`, league).subscribe(() => {
      this.getLeagues();
    });
  }

  deleteLeague(id: number): void {
    this.http.delete<any>(`/api/leagues/${id}`).subscribe(() => {
      this.getLeagues();
    });
  }
}