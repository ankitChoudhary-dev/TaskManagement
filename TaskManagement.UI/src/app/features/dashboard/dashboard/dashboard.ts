import { Component, OnInit } from '@angular/core';
import { Dashboard as DashboardService } from '../../../core/services/dashboard';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class Dashboard implements OnInit {

  constructor(
    private dashboardService: DashboardService
  ) {}


  ngOnInit(): void {

    this.dashboardService.getDashboard()
      .subscribe({
        next: (response) => {
          console.log("Dashboard Data", response);
        },
        error: (error) => {
          console.log("Dashboard Error", error);
        }
      });

  }

}