import React, { FunctionComponent, useState } from "react";
import { Badge, Popover, PopoverBody, PopoverHeader } from "reactstrap";
import Balance from "components/Balance";
import DepositMoney from "./Deposit";
import WithdrawMoney from "./Withdraw";
import { CURRENCY_TYPE_MONEY, TRANSACTION_STATUS_SUCCEEDED, TRANSACTION_STATUS_PENDING } from "types";

const MoneyBreadcrumb: FunctionComponent<any> = ({ className }) => {
  const [open, setOpen] = useState(false);
  return (
    <div className={className}>
      <Badge
        id="moneyPopover"
        className="bg-gray-900"
        style={{ width: "100px" }}
      >
        <Balance
          currencyType={CURRENCY_TYPE_MONEY}
          transactionStatus={TRANSACTION_STATUS_SUCCEEDED}
        />
      </Badge>
      <Popover
        style={{
          width: "250px"
        }}
        placement="bottom"
        isOpen={open}
        target="moneyPopover"
        trigger="hover"
        delay={{ show: 0, hide: 250 }}
        toggle={() => setOpen(!open)}
      >
        <PopoverHeader className="text-uppercase">MONEY BALANCE</PopoverHeader>
        <PopoverBody>
          <dl className="row mb-0">
            <dt className="col-6">Available</dt>
            <dd className="col-6">
              <Balance
                currencyType={CURRENCY_TYPE_MONEY}
                transactionStatus={TRANSACTION_STATUS_SUCCEEDED}
                alignment="right"
              />
            </dd>
            <dt className="col-6">Pending</dt>
            <dd className="col-6">
              <Balance
                currencyType={CURRENCY_TYPE_MONEY}
                transactionStatus={TRANSACTION_STATUS_PENDING}
                alignment="right"
              />
            </dd>
          </dl>
          <p className="text-muted text-justify">
            Pending transactions are in the process of getting validated. If the
            transaction stay for longer than 5 minutes please contact{" "}
            <a href="mailto:support@edoxa.gg">support@edoxa.gg</a>.
          </p>
          <DepositMoney />
          <WithdrawMoney />
        </PopoverBody>
      </Popover>
    </div>
  );
};

export default MoneyBreadcrumb;
