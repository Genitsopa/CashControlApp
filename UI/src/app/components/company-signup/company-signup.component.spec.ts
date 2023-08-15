import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanySignupComponent } from './company-signup.component';

describe('CompanySignupComponent', () => {
  let component: CompanySignupComponent;
  let fixture: ComponentFixture<CompanySignupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CompanySignupComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CompanySignupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
