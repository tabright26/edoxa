import React, { FunctionComponent } from "react";
import PaymentMethodCardPanel from "components/Payment/Stripe/PaymentMethod/Card/Panel";

const PaymentMethods: FunctionComponent = () => (
  <>
    <h5 className="text-uppercase my-4">PAYMENT METHODS</h5>
    <PaymentMethodCardPanel className="my-4" />
    {/* <BankAccountPanel className="my-4" /> */}
  </>
);

export default PaymentMethods;
