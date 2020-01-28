import React, { useState, FunctionComponent, useEffect } from "react";
import { Card, CardHeader, CardBody } from "reactstrap";
import DoxatagForm from "components/User/Doxatag/Form";
import { compose } from "recompose";
import { Loading } from "components/Shared/Loading";
import { connect } from "react-redux";
import { RootState } from "store/types";
import { loadUserDoxatagHistory } from "store/actions/identity";
import { produce, Draft } from "immer";
import { UserDoxatag } from "types";

const Doxatag: FunctionComponent<any> = ({
  className,
  doxatag: { data, loading },
  loadUserDoxatagHistory
}) => {
  useEffect((): void => {
    if (data === null) {
      loadUserDoxatagHistory();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
  const [buttonDisabled, setButtonDisabled] = useState(false);
  const disabled = !data || buttonDisabled;
  return (
    <Card className={`card-accent-primary ${className}`}>
      <CardHeader className="d-flex">
        <strong className="text-uppercase my-auto">DOXATAG</strong>
        {/* <Button.Link
          className="p-0 ml-auto my-auto"
          icon={faEdit}
          disabled={disabled}
          onClick={() => setButtonDisabled(true)}
        >
          UPDATE
        </Button.Link> */}
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
                  {data.name}#{data.code}
                </span>
              )}
            </dd>
          </dl>
        )}
      </CardBody>
    </Card>
  );
};

const mapStateToProps = (state: RootState) => {
  const { data, error, loading } = state.root.user.doxatagHistory;
  const doxatags = produce(data, (draft: Draft<UserDoxatag[]>) => {
    draft.sort((left: UserDoxatag, right: UserDoxatag) =>
      left.timestamp < right.timestamp ? 1 : -1
    );
  });
  return {
    doxatag: {
      data: doxatags[0] || null,
      error,
      loading
    }
  };
};

const mapDispatchToProps = dispatch => {
  return {
    loadUserDoxatagHistory: () => dispatch(loadUserDoxatagHistory())
  };
};

const enhance = compose<any, any>(connect(mapStateToProps, mapDispatchToProps));

export default enhance(Doxatag);
