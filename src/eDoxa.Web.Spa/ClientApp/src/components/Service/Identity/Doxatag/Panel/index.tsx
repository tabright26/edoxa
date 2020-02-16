import React, { useState, FunctionComponent, useEffect } from "react";
import { Card, CardHeader, CardBody } from "reactstrap";
import DoxatagForm from "components/Service/Identity/Doxatag/Form";
import { compose } from "recompose";
import { Loading } from "components/Shared/Loading";
import { connect, MapDispatchToProps, MapStateToProps } from "react-redux";
import { RootState } from "store/types";
import { loadUserDoxatagHistory } from "store/actions/identity";
import { produce, Draft } from "immer";
import { Doxatag } from "types/identity";

type OwnProps = {};

type StateProps = {
  doxatag?: Doxatag;
  loading: boolean;
};

type DispatchProps = {
  loadUserDoxatagHistory: () => void;
};

type InnerProps = StateProps & DispatchProps;

type OutterProps = {
  className?: string;
};

type Props = InnerProps & OutterProps;

const Panel: FunctionComponent<Props> = ({
  className,
  doxatag,
  loading,
  loadUserDoxatagHistory
}) => {
  useEffect((): void => {
    if (doxatag === null) {
      loadUserDoxatagHistory();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
  const [buttonDisabled, setButtonDisabled] = useState(false);
  const disabled = !doxatag || buttonDisabled;
  return (
    <Card className={`card-accent-primary ${className}`}>
      <CardHeader className="d-flex">
        <strong className="text-uppercase my-auto">DOXATAG</strong>
      </CardHeader>
      <CardBody>
        {loading ? (
          <Loading />
        ) : (
          <dl className="row mb-0">
            <dd className="col-sm-3 mb-0 text-muted">DoxaTag</dd>
            <dd className="col-sm-5 mb-0">
              {disabled && (
                <DoxatagForm.Update
                  handleCancel={() => setButtonDisabled(false)}
                />
              )}
              {!disabled && (
                <span>
                  {doxatag.name}#{doxatag.code}
                </span>
              )}
            </dd>
          </dl>
        )}
      </CardBody>
    </Card>
  );
};

const mapStateToProps: MapStateToProps<
  StateProps,
  OwnProps,
  RootState
> = state => {
  const { data, loading } = state.root.user.doxatagHistory;
  const doxatags = produce(data, (draft: Draft<Doxatag[]>) => {
    draft.sort((left: Doxatag, right: Doxatag) =>
      left.timestamp < right.timestamp ? 1 : -1
    );
  });
  const doxatag = doxatags[0] || null;
  return {
    doxatag,
    loading
  };
};

const mapDispatchToProps: MapDispatchToProps<DispatchProps, OwnProps> = (
  dispatch: any
) => {
  return {
    loadUserDoxatagHistory: () => dispatch(loadUserDoxatagHistory())
  };
};

const enhance = compose<InnerProps, OutterProps>(
  connect(mapStateToProps, mapDispatchToProps)
);

export default enhance(Panel);
