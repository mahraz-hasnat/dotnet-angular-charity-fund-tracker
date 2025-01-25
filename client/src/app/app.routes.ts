import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { ListsComponent } from './lists/lists.component';
import { PaymentsComponent } from './payments/payments.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'members', component: MemberListComponent },
  { path: 'member/:id', component: MemberDetailComponent },
  { path: 'lists', component: ListsComponent },
  { path: 'payments', component: PaymentsComponent },
  { path: '**', component: HomeComponent, pathMatch: 'full' },
];
