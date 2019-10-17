import React, { Fragment, Suspense } from "react";
import Loading from "components/Shared/Override/Loading";
import { STRIPE_PAYMENTMETHOD_CARD_TYPE } from "store/root/payment/stripe/paymentMethods/types";
import StripePaymentMethodModal from "modals/Payment/Stripe/PaymentMethod";
import { Elements } from "react-stripe-elements";

const Cards = React.lazy(() => import("./Cards"));
const BankAccount = React.lazy(() => import("./BankAccount"));

const PaymentMethods = () => (
  <Fragment>
    <h5 className="text-uppercase">PAYMENT METHODS</h5>
    <Elements>
      <StripePaymentMethodModal.Create />
    </Elements>
    <StripePaymentMethodModal.Update />
    <StripePaymentMethodModal.Delete />
    <Suspense fallback={<Loading />}>
      <Cards className="my-4" paymentMethodType={STRIPE_PAYMENTMETHOD_CARD_TYPE} />
    </Suspense>
    <Suspense fallback={<Loading />}>
      <Elements>
        <BankAccount className="my-4" />
      </Elements>
    </Suspense>
  </Fragment>
);

export default PaymentMethods;
