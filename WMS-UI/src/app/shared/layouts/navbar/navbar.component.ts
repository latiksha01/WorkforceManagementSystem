import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatDividerModule } from '@angular/material/divider';
import { MatTooltipModule } from '@angular/material/tooltip';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [MatButtonModule, MatIconModule, MatMenuModule, MatDividerModule, MatTooltipModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  username = '';
  role = '';

  constructor(private authService: AuthService, private router: Router) {
    this.username = this.authService.getUsername();
    this.role = this.authService.getRole();
  }

  getInitial(): string {
    return this.username.charAt(0).toUpperCase() || 'U';
  }

  changePassword(): void {
  this.router.navigate(['/change-password']);
}

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
