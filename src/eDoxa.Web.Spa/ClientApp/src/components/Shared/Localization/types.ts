export interface Country {
  readonly name: string;
  readonly twoDigitIso: string;
}

export interface Address {
  country: Country;
  line1: string;
  line2: string | null;
  city: string;
  state: string | null;
  postalCode: string;
}
