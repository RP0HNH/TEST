PGDMP      
            	    |            postgres    17.0    17.0                 0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                           false                       0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                           false                       0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                           false                       1262    5    postgres    DATABASE     |   CREATE DATABASE postgres WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Russia.1251';
    DROP DATABASE postgres;
                     postgres    false                       0    0    DATABASE postgres    COMMENT     N   COMMENT ON DATABASE postgres IS 'default administrative connection database';
                        postgres    false    4867            �            1259    16390    Files    TABLE       CREATE TABLE public."Files" (
    "Id" integer NOT NULL,
    "Name" character varying(255) NOT NULL,
    "Author" character varying(255) NOT NULL,
    "UploadDate" timestamp without time zone NOT NULL,
    "Size" bigint NOT NULL,
    "Format" character varying(10) NOT NULL
);
    DROP TABLE public."Files";
       public         heap r       postgres    false            �            1259    16395    Users    TABLE     �   CREATE TABLE public."Users" (
    "Id" integer NOT NULL,
    "FirstName" character varying(255) NOT NULL,
    "LastName" character varying(255) NOT NULL,
    "Biography" text
);
    DROP TABLE public."Users";
       public         heap r       postgres    false            �            1259    16408    __EFMigrationsHistory    TABLE     �   CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);
 +   DROP TABLE public."__EFMigrationsHistory";
       public         heap r       postgres    false            �            1259    16400    cloudfiles_id_seq    SEQUENCE     �   CREATE SEQUENCE public.cloudfiles_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE public.cloudfiles_id_seq;
       public               postgres    false    217                       0    0    cloudfiles_id_seq    SEQUENCE OWNED BY     F   ALTER SEQUENCE public.cloudfiles_id_seq OWNED BY public."Files"."Id";
          public               postgres    false    219            �            1259    16401    users_id_seq    SEQUENCE     �   CREATE SEQUENCE public.users_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 #   DROP SEQUENCE public.users_id_seq;
       public               postgres    false    218                       0    0    users_id_seq    SEQUENCE OWNED BY     A   ALTER SEQUENCE public.users_id_seq OWNED BY public."Users"."Id";
          public               postgres    false    220            `           2604    16402    Files Id    DEFAULT     m   ALTER TABLE ONLY public."Files" ALTER COLUMN "Id" SET DEFAULT nextval('public.cloudfiles_id_seq'::regclass);
 ;   ALTER TABLE public."Files" ALTER COLUMN "Id" DROP DEFAULT;
       public               postgres    false    219    217            a           2604    16403    Users Id    DEFAULT     h   ALTER TABLE ONLY public."Users" ALTER COLUMN "Id" SET DEFAULT nextval('public.users_id_seq'::regclass);
 ;   ALTER TABLE public."Users" ALTER COLUMN "Id" DROP DEFAULT;
       public               postgres    false    220    218            �          0    16390    Files 
   TABLE DATA           Y   COPY public."Files" ("Id", "Name", "Author", "UploadDate", "Size", "Format") FROM stdin;
    public               postgres    false    217   �       �          0    16395    Users 
   TABLE DATA           M   COPY public."Users" ("Id", "FirstName", "LastName", "Biography") FROM stdin;
    public               postgres    false    218   '       �          0    16408    __EFMigrationsHistory 
   TABLE DATA           R   COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
    public               postgres    false    221   �                  0    0    cloudfiles_id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public.cloudfiles_id_seq', 4, true);
          public               postgres    false    219                       0    0    users_id_seq    SEQUENCE SET     ;   SELECT pg_catalog.setval('public.users_id_seq', 1, false);
          public               postgres    false    220            g           2606    16412 .   __EFMigrationsHistory PK___EFMigrationsHistory 
   CONSTRAINT     {   ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");
 \   ALTER TABLE ONLY public."__EFMigrationsHistory" DROP CONSTRAINT "PK___EFMigrationsHistory";
       public                 postgres    false    221            c           2606    16405    Files cloudfiles_pkey 
   CONSTRAINT     W   ALTER TABLE ONLY public."Files"
    ADD CONSTRAINT cloudfiles_pkey PRIMARY KEY ("Id");
 A   ALTER TABLE ONLY public."Files" DROP CONSTRAINT cloudfiles_pkey;
       public                 postgres    false    217            e           2606    16407    Users users_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY public."Users"
    ADD CONSTRAINT users_pkey PRIMARY KEY ("Id");
 <   ALTER TABLE ONLY public."Users" DROP CONSTRAINT users_pkey;
       public                 postgres    false    218            �   z   x�3�0��.̽0O�������͟����D��@��T��������B�����Č�؜��˄� #�$?���R��"��P��B��L/� ��1(�3+##�1�@c�,M�8A*�b���� ��"�      �   X   x�U�1@@뿇�p�YZ��"6,��0�F~B���{S
3�����*\&NmX�������&���F��S�Y�Ł�c�úpν�NV      �      x������ � �     