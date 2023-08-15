import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html',
  styleUrls: ['./teams.component.css']
})
export class TeamsComponent implements OnInit {
  teams: any[] | undefined;
  form: any = {};

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getTeams();
  }

  getTeams(): void {
    this.http.get<any[]>('/api/teams').subscribe(teams => {
      this.teams = teams;
    });
  }

  createTeam(team: any): void {
    this.http.post<any>('/api/teams', team).subscribe(() => {
      this.getTeams();
    });
  }

  updateTeam(team: any): void {
    this.http.put<any>(`/api/teams/${team.id}`, team).subscribe(() => {
      this.getTeams();
    });
  }

  deleteTeam(id: number): void {
    this.http.delete<any>(`/api/teams/${id}`).subscribe(() => {
      this.getTeams();
    });
  }
}