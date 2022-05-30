import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/memebr';


@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;
  members: Member[] = [];

  constructor(private http: HttpClient) { }


  getMembers(): Observable<Member[]> {
    if (this.members.length > 0) return of(this.members); // use "of" operator to convert to observable sequence
    return this.http.get<Member[]>(this.baseUrl + "users").pipe(
      map(members => {
        this.members = members;
        return members;
      })
    )
  }

  getMember(username: string): Observable<Member> {
    const member = this.members.find(x => x.username == username);
    if (member !== undefined) {
      return of(member);
    }
    return this.http.get<Member>(this.baseUrl + "users/" + username);
  }

  updateMember(member: Member) {
    return this.http.put(this.baseUrl + 'users', member).pipe(
      map(() => {
        const index = this.members.indexOf(member);
        this.members[index] = member;
      })
    )
  }
}
