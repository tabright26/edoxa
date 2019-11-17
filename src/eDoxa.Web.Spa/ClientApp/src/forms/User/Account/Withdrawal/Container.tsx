import { connect, MapStateToProps } from "react-redux";
import { USER_ACCOUNT_WITHDRAWAL_MONEY_FAIL, UserAccountWithdrawalActions } from "store/root/user/account/withdrawal/types";
import { accountWithdrawal } from "store/root/user/account/withdrawal/actions";
import Withdrawal from "./Withdrawal";
import { RootState } from "store/types";
import { Bundle, Currency } from "types";
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

const mapStateToProps: MapStateToProps<StateProps, OwnProps, RootState> = (state, ownProps) => {
  return {
    initialValues: {
      bundle: ownProps.bundles[0].amount
    }
  };
};

const mapDispatchToProps = (dispatch: any, ownProps: OwnProps) => {
  return {
    accountWithdrawal: (data: any) =>
      dispatch(accountWithdrawal(ownProps.currency, data.bundle)).then((action: UserAccountWithdrawalActions) => {
        switch (action.type) {
          case USER_ACCOUNT_WITHDRAWAL_MONEY_FAIL: {
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
)(Withdrawal);
