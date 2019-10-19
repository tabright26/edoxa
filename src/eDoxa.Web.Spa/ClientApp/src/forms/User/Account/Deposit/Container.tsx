import { connect, MapStateToProps } from "react-redux";
import { accountDeposit } from "store/root/user/account/deposit/actions";
import { USER_ACCOUNT_DEPOSIT_MONEY_FAIL, USER_ACCOUNT_DEPOSIT_TOKEN_FAIL, UserAccountDepositActions } from "store/root/user/account/deposit/types";
import Deposit from "./Deposit";
import { RootState } from "store/types";
import { Currency, Bundle } from "types";
import { throwSubmissionError } from "utils/form/types";

interface OwnProps {
  currency: Currency;
  bundles: Bundle[];
}

interface StateProps {
  initialValues: {
    bundle: number;
  };
}

const mapStateToProps: MapStateToProps<StateProps, OwnProps, RootState> = (state: RootState, ownProps: OwnProps) => {
  return {
    initialValues: {
      bundle: ownProps.bundles[0].amount
    }
  };
};

const mapDispatchToProps = (dispatch: any, ownProps: OwnProps) => {
  return {
    accountDeposit: (data: any) =>
      dispatch(accountDeposit(ownProps.currency, data.bundle)).then((action: UserAccountDepositActions) => {
        switch (action.type) {
          case USER_ACCOUNT_DEPOSIT_MONEY_FAIL:
          case USER_ACCOUNT_DEPOSIT_TOKEN_FAIL: {
            throwSubmissionError(action.error);
            break;
          }
        }
      })
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(Deposit);
