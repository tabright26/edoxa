import React from "react";
import { Button } from "reactstrap";
import { withModals } from "utils/modal/container";
import { compose } from "recompose";
import { CURRENCY_MONEY } from "types";
import { withUserAccountDepositBundles } from "store/root/user/account/deposit/bundles/container";
import { withStripeCustomerHasDefaultPaymentMethod } from "store/root/payment/stripe/customer/container";

const DepositButton = ({ modals, bundles: { data, loading }, hasDefaultPaymentMethod }) => (
  <Button color="primary" size="sm" disabled={loading || !hasDefaultPaymentMethod} block onClick={() => modals.showDepositModal(CURRENCY_MONEY, data)}>
    Deposit Money
  </Button>
);

const enhance = compose<any, any>(
  withModals,
  withUserAccountDepositBundles,
  withStripeCustomerHasDefaultPaymentMethod
);

export default enhance(DepositButton);
