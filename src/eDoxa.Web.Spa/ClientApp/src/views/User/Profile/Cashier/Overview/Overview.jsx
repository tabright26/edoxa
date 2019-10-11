import React, { Suspense } from "react";

const Verification = React.lazy(() => import("./Verification"));
const BankAccount = React.lazy(() => import("./BankAccount"));

const Overview = () => (
  <>
    <h5>CASHIER OVERVIEW</h5>
    <Suspense>
      <Verification />
    </Suspense>
    <Suspense>
      <BankAccount />
    </Suspense>
  </>
);

export default Overview;
