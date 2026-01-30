import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Benutzer } from './benutzer';

describe('Benutzer', () => {
  let component: Benutzer;
  let fixture: ComponentFixture<Benutzer>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Benutzer]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Benutzer);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
