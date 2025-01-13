--
-- PostgreSQL database dump
--

-- Dumped from database version 16.0
-- Dumped by pg_dump version 16.0

-- Started on 2025-01-13 13:27:56

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;




SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 225 (class 1259 OID 25553)
-- Name: meta; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.meta (
    column_name character varying(50),
    table_name character varying(50),
    column_type character varying(50)
);


--
-- TOC entry 224 (class 1259 OID 25442)
-- Name: Запланированные_задачи; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public."Запланированные_задачи" (
    "id_задачи" integer NOT NULL,
    "id_сотрудника" integer,
    "Описание_задачи" character varying(255),
    "Статус_задачи" character varying(50),
    "Дата_и_время_начала" timestamp without time zone,
    "Дата_и_время_окончания" timestamp without time zone
);


--
-- TOC entry 223 (class 1259 OID 25441)
-- Name: Запланированные_задачи_id_задачи_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public."Запланированные_задачи_id_задачи_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 4854 (class 0 OID 0)
-- Dependencies: 223
-- Name: Запланированные_задачи_id_задачи_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public."Запланированные_задачи_id_задачи_seq" OWNED BY public."Запланированные_задачи"."id_задачи";


--
-- TOC entry 220 (class 1259 OID 25403)
-- Name: Заявки_на_обслуживание; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public."Заявки_на_обслуживание" (
    "id_заявки" integer NOT NULL,
    "id_сотрудника" integer,
    "id_устройства" integer,
    "Описание_проблемы" character varying(255),
    "Статус_заявки" character varying(50)
);


--
-- TOC entry 219 (class 1259 OID 25402)
-- Name: Заявки_на_обслуживание_id_заявки_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public."Заявки_на_обслуживание_id_заявки_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 4855 (class 0 OID 0)
-- Dependencies: 219
-- Name: Заявки_на_обслуживание_id_заявки_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public."Заявки_на_обслуживание_id_заявки_seq" OWNED BY public."Заявки_на_обслуживание"."id_заявки";


--
-- TOC entry 222 (class 1259 OID 25420)
-- Name: История_обслуживания; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public."История_обслуживания" (
    "id_записи" integer NOT NULL,
    "id_заявки" integer,
    "id_сотрудника" integer,
    "id_устройства" integer,
    "Дата_и_время_обращения" timestamp without time zone,
    "Описание_работ" character varying(255)
);


--
-- TOC entry 221 (class 1259 OID 25419)
-- Name: История_обслуживания_id_записи_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public."История_обслуживания_id_записи_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 4856 (class 0 OID 0)
-- Dependencies: 221
-- Name: История_обслуживания_id_записи_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public."История_обслуживания_id_записи_seq" OWNED BY public."История_обслуживания"."id_записи";


--
-- TOC entry 228 (class 1259 OID 25595)
-- Name: Пользователи; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public."Пользователи" (
    "id_пользователя" integer NOT NULL,
    "Логин" character varying(50),
    "Пароль" character varying(50),
    "Статус" character varying(50)
);


--
-- TOC entry 227 (class 1259 OID 25594)
-- Name: Пользователи_id_пользователя_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public."Пользователи_id_пользователя_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 4857 (class 0 OID 0)
-- Dependencies: 227
-- Name: Пользователи_id_пользователя_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public."Пользователи_id_пользователя_seq" OWNED BY public."Пользователи"."id_пользователя";


--
-- TOC entry 226 (class 1259 OID 25559)
-- Name: Связи; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public."Связи" (
    table_name1 character varying(250),
    table_name2 character varying(250),
    relations character varying(250),
    via character varying(250)
);


--
-- TOC entry 216 (class 1259 OID 25382)
-- Name: Сотрудники; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public."Сотрудники" (
    "id_сотрудника" integer NOT NULL,
    "Имя" character varying(50),
    "Фамилия" character varying(50),
    "Должность" character varying(50),
    "Отдел" character varying(50)
);


--
-- TOC entry 215 (class 1259 OID 25381)
-- Name: Сотрудники_id_сотрудника_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public."Сотрудники_id_сотрудника_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 4858 (class 0 OID 0)
-- Dependencies: 215
-- Name: Сотрудники_id_сотрудника_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public."Сотрудники_id_сотрудника_seq" OWNED BY public."Сотрудники"."id_сотрудника";


--
-- TOC entry 218 (class 1259 OID 25389)
-- Name: Устройства; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public."Устройства" (
    "id_устройства" integer NOT NULL,
    "Название" character varying(50),
    "Тип" character varying(50),
    "Серийный_номер" character varying(50),
    "Дата_приобретения" date
);


--
-- TOC entry 217 (class 1259 OID 25388)
-- Name: Устройства_id_устройства_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public."Устройства_id_устройства_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 4859 (class 0 OID 0)
-- Dependencies: 217
-- Name: Устройства_id_устройства_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public."Устройства_id_устройства_seq" OWNED BY public."Устройства"."id_устройства";


--
-- TOC entry 4671 (class 2604 OID 25445)
-- Name: Запланированные_задачи id_задачи; Type: DEFAULT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."Запланированные_задачи" ALTER COLUMN "id_задачи" SET DEFAULT nextval('public."Запланированные_задачи_id_задачи_seq"'::regclass);


--
-- TOC entry 4669 (class 2604 OID 25406)
-- Name: Заявки_на_обслуживание id_заявки; Type: DEFAULT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."Заявки_на_обслуживание" ALTER COLUMN "id_заявки" SET DEFAULT nextval('public."Заявки_на_обслуживание_id_заявки_seq"'::regclass);


--
-- TOC entry 4670 (class 2604 OID 25423)
-- Name: История_обслуживания id_записи; Type: DEFAULT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."История_обслуживания" ALTER COLUMN "id_записи" SET DEFAULT nextval('public."История_обслуживания_id_записи_seq"'::regclass);


--
-- TOC entry 4672 (class 2604 OID 25598)
-- Name: Пользователи id_пользователя; Type: DEFAULT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."Пользователи" ALTER COLUMN "id_пользователя" SET DEFAULT nextval('public."Пользователи_id_пользователя_seq"'::regclass);


--
-- TOC entry 4667 (class 2604 OID 25385)
-- Name: Сотрудники id_сотрудника; Type: DEFAULT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."Сотрудники" ALTER COLUMN "id_сотрудника" SET DEFAULT nextval('public."Сотрудники_id_сотрудника_seq"'::regclass);


--
-- TOC entry 4668 (class 2604 OID 25392)
-- Name: Устройства id_устройства; Type: DEFAULT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."Устройства" ALTER COLUMN "id_устройства" SET DEFAULT nextval('public."Устройства_id_устройства_seq"'::regclass);


--
-- TOC entry 4844 (class 0 OID 25553)
-- Dependencies: 225
-- Data for Name: meta; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public.meta VALUES ('Описание_задачи', 'Запланированные_задачи', 'C');
INSERT INTO public.meta VALUES ('Дата_и_время_начала', 'Запланированные_задачи', 'D');
INSERT INTO public.meta VALUES ('Дата_и_время_окончания', 'Запланированные_задачи', 'D');
INSERT INTO public.meta VALUES ('Имя', 'Сотрудники', 'C');
INSERT INTO public.meta VALUES ('Фамилия', 'Сотрудники', 'C');
INSERT INTO public.meta VALUES ('Должность', 'Сотрудники', 'C');
INSERT INTO public.meta VALUES ('Отдел', 'Сотрудники', 'C');
INSERT INTO public.meta VALUES ('Дата_и_время_обращения', 'История_обслуживания', 'D');
INSERT INTO public.meta VALUES ('Описание_работ', 'История_обслуживания', 'C');
INSERT INTO public.meta VALUES ('Описание_проблемы', 'Заявки_на_обслуживание', 'C');
INSERT INTO public.meta VALUES ('Название', 'Устройства', 'C');
INSERT INTO public.meta VALUES ('Тип', 'Устройства', 'C');
INSERT INTO public.meta VALUES ('Серийный_номер', 'Устройства', 'C');
INSERT INTO public.meta VALUES ('Дата_приобретения', 'Устройства', 'D');
INSERT INTO public.meta VALUES ('Статус_задачи', 'Запланированные_задачи', 'C');
INSERT INTO public.meta VALUES ('Статус_заявки', 'Заявки_на_обслуживание', 'C');


--
-- TOC entry 4843 (class 0 OID 25442)
-- Dependencies: 224
-- Data for Name: Запланированные_задачи; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."Запланированные_задачи" VALUES (2, 2, 'Написать программу для обработки данных', 'Запланировано', '2021-03-02 09:00:00', '2021-03-02 12:00:00');
INSERT INTO public."Запланированные_задачи" VALUES (3, 3, 'Провести собеседование с кандидатом', 'Выполнено', '2021-03-03 10:30:00', '2021-03-03 11:30:00');
INSERT INTO public."Запланированные_задачи" VALUES (4, 3, 'Провести встречу с клиентами', 'Запланировано', '2021-08-01 15:00:00', '2021-08-01 17:00:00');
INSERT INTO public."Запланированные_задачи" VALUES (5, 3, 'Провести встречу', 'Запланировано', '2021-08-01 15:00:00', '2021-08-01 15:00:00');


--
-- TOC entry 4839 (class 0 OID 25403)
-- Dependencies: 220
-- Data for Name: Заявки_на_обслуживание; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."Заявки_на_обслуживание" VALUES (2, 2, 2, 'Батарея быстро разряжается', 'В ожидании');
INSERT INTO public."Заявки_на_обслуживание" VALUES (3, 3, 3, 'Не печатает долго', 'Завершено');
INSERT INTO public."Заявки_на_обслуживание" VALUES (4, 2, 2, 'сломался', 'в ожидании');


--
-- TOC entry 4841 (class 0 OID 25420)
-- Dependencies: 222
-- Data for Name: История_обслуживания; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."История_обслуживания" VALUES (2, 2, 2, 2, '2020-04-21 14:45:00', 'Замена батареи');
INSERT INTO public."История_обслуживания" VALUES (3, 3, 3, 3, '2019-08-11 09:15:00', 'Замена тонера');
INSERT INTO public."История_обслуживания" VALUES (4, 3, 2, 1, '2019-08-11 09:15:00', 'Замена тонера');


--
-- TOC entry 4847 (class 0 OID 25595)
-- Dependencies: 228
-- Data for Name: Пользователи; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."Пользователи" VALUES (1, 'Логин', 'Пароль', 'HR');
INSERT INTO public."Пользователи" VALUES (2, 'login', 'password', 'Сотрудник');


--
-- TOC entry 4845 (class 0 OID 25559)
-- Dependencies: 226
-- Data for Name: Связи; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."Связи" VALUES ('Запланированные_задачи', 'Сотрудники', 'Сотрудники.id_сотрудника=Запланированные_задачи.id_сотрудника', '-');
INSERT INTO public."Связи" VALUES ('Заявки_на_обслуживание', 'Сотрудники', 'Заявки_на_обслуживание.id_сотрудника=Сотрудники.id_сотрудника', '-');
INSERT INTO public."Связи" VALUES ('Заявки_на_обслуживание', 'Устройства', 'Заявки_на_обслуживание.id_устройства=Устройства.id_устройства', '-');
INSERT INTO public."Связи" VALUES ('История_обслуживания', 'Заявки_на_обслуживание', 'История_обслуживания.id_заявки=Заявки_на_обслуживание.id_заявки', '-');
INSERT INTO public."Связи" VALUES ('История_обслуживания', 'Сотрудники', 'История_обслуживания.id_сотрудника=Сотрудники.id_сотрудника', '-');
INSERT INTO public."Связи" VALUES ('История_обслуживания', 'Устройства', 'История_обслуживания.id_устройства=Устройства.id_устройства', '-');
INSERT INTO public."Связи" VALUES ('Запланированные_задачи', 'Заявки_на_обслуживание', '-', 'Сотрудники');
INSERT INTO public."Связи" VALUES ('Запланированные_задачи', 'История_обслуживания', '-', 'Сотрудники');
INSERT INTO public."Связи" VALUES ('Сотрудники', 'Устройства', '-', 'Заявки_на_обслуживание');


--
-- TOC entry 4835 (class 0 OID 25382)
-- Dependencies: 216
-- Data for Name: Сотрудники; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."Сотрудники" VALUES (2, 'Петр', 'Петров', 'Разработчик', 'IT отдел');
INSERT INTO public."Сотрудники" VALUES (3, 'Анастасия', 'Сидорова', 'HR-специалист', 'Отдел персонала');
INSERT INTO public."Сотрудники" VALUES (4, 'Иван ', 'Иванов', 'Разработчик', 'IT отдел');
INSERT INTO public."Сотрудники" VALUES (5, 'Матвей', 'Томилов', 'Разработчик', 'IT отдел');


--
-- TOC entry 4837 (class 0 OID 25389)
-- Dependencies: 218
-- Data for Name: Устройства; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."Устройства" VALUES (1, 'Ноутбук', 'Персональный компьютер', 'AB123CD', '2021-01-15');
INSERT INTO public."Устройства" VALUES (2, 'Смартфон', 'Мобильное устройство', 'EF456GH', '2020-04-20');
INSERT INTO public."Устройства" VALUES (3, 'Принтер', 'Периферийное устройство', 'IJ789KL', '2019-08-10');


--
-- TOC entry 4860 (class 0 OID 0)
-- Dependencies: 223
-- Name: Запланированные_задачи_id_задачи_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."Запланированные_задачи_id_задачи_seq"', 1, false);


--
-- TOC entry 4861 (class 0 OID 0)
-- Dependencies: 219
-- Name: Заявки_на_обслуживание_id_заявки_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."Заявки_на_обслуживание_id_заявки_seq"', 1, false);


--
-- TOC entry 4862 (class 0 OID 0)
-- Dependencies: 221
-- Name: История_обслуживания_id_записи_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."История_обслуживания_id_записи_seq"', 1, false);


--
-- TOC entry 4863 (class 0 OID 0)
-- Dependencies: 227
-- Name: Пользователи_id_пользователя_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."Пользователи_id_пользователя_seq"', 1, false);


--
-- TOC entry 4864 (class 0 OID 0)
-- Dependencies: 215
-- Name: Сотрудники_id_сотрудника_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."Сотрудники_id_сотрудника_seq"', 1, false);


--
-- TOC entry 4865 (class 0 OID 0)
-- Dependencies: 217
-- Name: Устройства_id_устройства_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."Устройства_id_устройства_seq"', 1, false);


--
-- TOC entry 4682 (class 2606 OID 25447)
-- Name: Запланированные_задачи Запланированные_задачи_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."Запланированные_задачи"
    ADD CONSTRAINT "Запланированные_задачи_pkey" PRIMARY KEY ("id_задачи");


--
-- TOC entry 4678 (class 2606 OID 25408)
-- Name: Заявки_на_обслуживание Заявки_на_обслуживание_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."Заявки_на_обслуживание"
    ADD CONSTRAINT "Заявки_на_обслуживание_pkey" PRIMARY KEY ("id_заявки");


--
-- TOC entry 4680 (class 2606 OID 25425)
-- Name: История_обслуживания История_обслуживания_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."История_обслуживания"
    ADD CONSTRAINT "История_обслуживания_pkey" PRIMARY KEY ("id_записи");


--
-- TOC entry 4684 (class 2606 OID 25600)
-- Name: Пользователи Пользователи_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."Пользователи"
    ADD CONSTRAINT "Пользователи_pkey" PRIMARY KEY ("id_пользователя");


--
-- TOC entry 4674 (class 2606 OID 25387)
-- Name: Сотрудники Сотрудники_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."Сотрудники"
    ADD CONSTRAINT "Сотрудники_pkey" PRIMARY KEY ("id_сотрудника");


--
-- TOC entry 4676 (class 2606 OID 25394)
-- Name: Устройства Устройства_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."Устройства"
    ADD CONSTRAINT "Устройства_pkey" PRIMARY KEY ("id_устройства");


--
-- TOC entry 4690 (class 2606 OID 25564)
-- Name: Запланированные_задачи Запланированные_з_id_сотрудника_fkey; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."Запланированные_задачи"
    ADD CONSTRAINT "Запланированные_з_id_сотрудника_fkey" FOREIGN KEY ("id_сотрудника") REFERENCES public."Сотрудники"("id_сотрудника") ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 4685 (class 2606 OID 25569)
-- Name: Заявки_на_обслуживание Заявки_на_обслужив_id_сотрудника_fkey; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."Заявки_на_обслуживание"
    ADD CONSTRAINT "Заявки_на_обслужив_id_сотрудника_fkey" FOREIGN KEY ("id_сотрудника") REFERENCES public."Сотрудники"("id_сотрудника") ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 4686 (class 2606 OID 25574)
-- Name: Заявки_на_обслуживание Заявки_на_обслужив_id_устройства_fkey; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."Заявки_на_обслуживание"
    ADD CONSTRAINT "Заявки_на_обслужив_id_устройства_fkey" FOREIGN KEY ("id_устройства") REFERENCES public."Устройства"("id_устройства") ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 4687 (class 2606 OID 25579)
-- Name: История_обслуживания История_обслужива_id_сотрудника_fkey; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."История_обслуживания"
    ADD CONSTRAINT "История_обслужива_id_сотрудника_fkey" FOREIGN KEY ("id_сотрудника") REFERENCES public."Сотрудники"("id_сотрудника") ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 4688 (class 2606 OID 25584)
-- Name: История_обслуживания История_обслужива_id_устройства_fkey; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."История_обслуживания"
    ADD CONSTRAINT "История_обслужива_id_устройства_fkey" FOREIGN KEY ("id_устройства") REFERENCES public."Устройства"("id_устройства") ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 4689 (class 2606 OID 25589)
-- Name: История_обслуживания История_обслуживания_id_заявки_fkey; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."История_обслуживания"
    ADD CONSTRAINT "История_обслуживания_id_заявки_fkey" FOREIGN KEY ("id_заявки") REFERENCES public."Заявки_на_обслуживание"("id_заявки") ON UPDATE CASCADE ON DELETE CASCADE;


-- Completed on 2025-01-13 13:27:56

--
-- PostgreSQL database dump complete
--

