import React, { FunctionComponent } from "react";
import { Button } from "reactstrap";
import { withModals } from "utils/modal/container";
import { compose } from "recompose";
import { MONEY } from "types";
import { withUserAccountWithdrawalBundles } from "store/root/user/account/withdrawal/bundles/container";

const Withdrawal: FunctionComponent<any> = ({ modals, bundles: { data, loading } }) => (
  <Button color="primary" size="sm" block disabled={loading} onClick={() => modals.showWithdrawalModal(MONEY, data)}>
    Withdrawal Money
  </Button>
);

const enhance = compose<any, any>(
  withModals,
  withUserAccountWithdrawalBundles
);

export default enhance(Withdrawal);
