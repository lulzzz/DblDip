import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/core/auth.service';
import { RedirectService } from 'src/app/core/redirect.service';

@Component({
  selector: 'app-workspace',
  templateUrl: './workspace.component.html',
  styleUrls: ['./workspace.component.scss']
})
export class WorkspaceComponent implements OnInit {

  constructor(private authService: AuthService, private redirectService: RedirectService) { }

  ngOnInit(): void {
  }

  public logout() {
    this.authService.logout();
    this.redirectService.redirectToLogin();
  }
}