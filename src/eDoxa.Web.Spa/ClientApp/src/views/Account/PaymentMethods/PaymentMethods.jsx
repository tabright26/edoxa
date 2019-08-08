import React from "react";

import CreateStripeCardModel from "../../../modals/Stripe/Card/Create/Create";

const AccountPaymentMethods = () => (
  <>
    <h1 className="animated fadeIn mt-4">Payment Methods</h1>
    <CreateStripeCardModel />
  </>
);

export default AccountPaymentMethods;
