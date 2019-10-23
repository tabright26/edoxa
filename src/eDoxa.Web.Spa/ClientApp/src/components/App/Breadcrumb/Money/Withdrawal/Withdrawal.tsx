import React, { FunctionComponent } from "react";
import { Button } from "reactstrap";
import { withModals } from "utils/modal/container";
import { compose } from "recompose";
import { MONEY } from "types";
import { withUserAccountWithdrawalBundles } from "store/root/user/account/withdrawal/bundles/container";
import { withStripeHasAccountVerified } from "store/root/payment/stripe/account/container";

const Withdrawal: FunctionComponent<any> = ({ modals, bundles: { data, loading }, hasAccountVerified }) => (
  <Button color="primary" size="sm" block disabled={loading || !hasAccountVerified} onClick={() => modals.showWithdrawalModal(MONEY, data)}>
    Withdrawal Money
  </Button>
);

const enhance = compose<any, any>(
  withModals,
  withUserAccountWithdrawalBundles,
  withStripeHasAccountVerified
);

export default enhance(Withdrawal);
