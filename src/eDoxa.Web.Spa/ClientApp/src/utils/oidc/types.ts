export const SUB_CLAIM_TYPE = "sub";
export const EMAIL_CLAIM_TYPE = "email";
export const COUNTRY_CLAIM_TYPE = "country";
export const DOXATAG_CLAIM_TYPE = "doxatag";

export type ClaimType =
  | typeof SUB_CLAIM_TYPE
  | typeof EMAIL_CLAIM_TYPE
  | typeof COUNTRY_CLAIM_TYPE
  | typeof DOXATAG_CLAIM_TYPE;
