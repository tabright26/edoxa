import { connect, MapStateToProps } from "react-redux";
import Deposit from "./Deposit";
import { RootState } from "store/types";
import { Currency, Bundle } from "types";

interface OwnProps {
  currency: Currency;
  bundles: Bundle[];
}

interface StateProps {
  initialValues: {
    bundle: number;
  };
}

const mapStateToProps: MapStateToProps<StateProps, OwnProps, RootState> = (
  state: RootState,
  ownProps: OwnProps
) => {
  return {
    initialValues: {
      bundle: ownProps.bundles[0].amount
    }
  };
};

export default connect(mapStateToProps)(Deposit);
