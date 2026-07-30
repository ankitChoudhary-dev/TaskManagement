import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Dashboard as DashboardService } from '../../../core/services/dashboard';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class Dashboard implements OnInit {
  stats: any = null;

  constructor(
    private dashboardService: DashboardService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.dashboardService.getDashboard()
      .subscribe({
        next: (response) => {
          console.log("Dashboard Data", response);
          this.stats = response;
          this.cdr.detectChanges(); // <--- Forces Angular to render the HTML immediately
        },
        error: (error) => {
          console.log("Dashboard Error", error);
        }
      });
  }
}