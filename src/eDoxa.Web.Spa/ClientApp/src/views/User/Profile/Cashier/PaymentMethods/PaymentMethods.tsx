import React, { Fragment, Suspense } from "react";
import Loading from "components/Shared/Override/Loading";
import StripePaymentMethodModal from "modals/Payment/Stripe/PaymentMethod";
import { Elements } from "react-stripe-elements";

const Cards = React.lazy(() => import("./Cards"));
const BankAccount = React.lazy(() => import("./BankAccount"));

const PaymentMethods = () => (
  <Fragment>
    <h5>PAYMENT METHODS</h5>
    <Elements>
      <StripePaymentMethodModal.Create />
    </Elements>
    <StripePaymentMethodModal.Update />
    <StripePaymentMethodModal.Delete />
    <Suspense fallback={<Loading />}>
      <Cards className="card-accent-primary my-4" />
    </Suspense>
    <Suspense fallback={<Loading />}>
      <Elements>
        <BankAccount />
      </Elements>
    </Suspense>
  </Fragment>
);

export default PaymentMethods;
