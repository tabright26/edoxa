import React, { Suspense } from "react";

const BankAccount = React.lazy(() => import("../PaymentMethods/BankAccount"));

const Overview = () => (
  <>
    <h5>CASHIER OVERVIEW</h5>
    <Suspense>
      <BankAccount />
    </Suspense>
  </>
);

export default Overview;
