import React, { Fragment, Suspense } from "react";
import Loading from "../../../../components/Shared/Loading";

import CreditCardModule from "./CreditCardModule";
import BankModule from "./BankModule";

const Cashier = ({ className, actions }) => (
  <Fragment>
    <Suspense fallback={<Loading.Default />}>
      <CreditCardModule />
    </Suspense>
    <Suspense fallback={<Loading.Default />}>
      <BankModule />
    </Suspense>
  </Fragment>
);

export default Cashier;
