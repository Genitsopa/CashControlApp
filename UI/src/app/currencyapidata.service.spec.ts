import { TestBed } from '@angular/core/testing';

import { CurrencyapidataService } from './currencyapidata.service';

describe('CurrencyapidataService', () => {
  let service: CurrencyapidataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CurrencyapidataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
