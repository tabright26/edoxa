import React from "react";
import { Button } from "reactstrap";
import { withModals } from "utils/modal/container";
import { compose } from "recompose";
import { TOKEN } from "types";
import { withUserAccountDepositBundles } from "store/root/user/account/deposit/bundles/container";
import { withStripeCustomerHasDefaultPaymentMethod } from "store/root/payment/stripe/customer/container";

const DepositButton = ({ modals, bundles: { data, loading }, hasDefaultPaymentMethod }) => (
  <Button color="primary" size="sm" block disabled={loading || !hasDefaultPaymentMethod} onClick={() => modals.showDepositModal(TOKEN, data)}>
    Buy Token
  </Button>
);

const enhance = compose<any, any>(
  withModals,
  withUserAccountDepositBundles,
  withStripeCustomerHasDefaultPaymentMethod
);

export default enhance(DepositButton);
